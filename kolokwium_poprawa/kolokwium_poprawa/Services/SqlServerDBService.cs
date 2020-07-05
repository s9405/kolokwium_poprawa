using kolokwium_poprawa.Model;
using kolokwium_poprawa.Requests;
using kolokwium_poprawa.Response;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium_poprawa.Services
{
    public class SqlServerDBService : IDataBaseService
    {

        public FireTruckResponse FireTruckToAction(FireTrucksRequest request)
        {
            var response = new FireTruckResponse();

            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s9405;Integrated Security=true"))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                var date = DateTime.Now;

                command.Parameters.AddWithValue("IdFireTruck", request.idFireTruck);
                command.Parameters.AddWithValue("IdAction", request.idAction);
                command.Parameters.AddWithValue("AssigmentDate", date);

                command.CommandText = "select max(IdFireTruckAction) from FireTruck_Action";
                var dr = command.ExecuteReader();
                int IdFireTruckAction = dr.GetInt32(0);
                command.Parameters.AddWithValue("IdFireTruckAction", IdFireTruckAction);
                dr.Close();

                command.CommandText = "Select * from FireTruck where IdFireTruck = @IdFireTruck";
                dr = command.ExecuteReader();
                if (!dr.HasRows)
                {
                    transaction.Rollback();
                    throw new ArgumentException("Brak podanego wozu");
                }
                dr.Close();


                command.CommandText = "Select * from Actions where IdActions = @IdActions";
                dr = command.ExecuteReader();
                if (!dr.HasRows)
                {
                    transaction.Rollback();
                    throw new ArgumentException("Brak podanej akcji");
                }
                dr.Close();




                command.CommandText = "INSERT INTO FireTruck_Action(IdFireTruckAction ,IdFireTruck,IdAction, AssigmentDate)" +
                    " values (@IdFireTruckAction,@IdFireTruck,@IdAction, @AssigmentDate)";
                dr = command.ExecuteReader();
                dr.Close();
                transaction.Commit();
                command.Parameters.Clear();
                connection.Close();

                response.IdAction = request.idAction;
                response.IdFireTruck = request.idFireTruck;
                response.AssignmentDate = date;
                response.IdFireTruckAction = IdFireTruckAction;

            }

                return response;
        }
        public List<FireFigthersActions> GetFireFigthersActions(int id)
        {
            var response = new List<FireFigthersActions>();

            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s9405;Integrated Security=true"))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "Select IdAction, StartTime, EndTime from Actions where IdAction in " +
                    "(select IdAction from Firefighter_Action where IdFirefigther = @id)";
                command.Parameters.AddWithValue("id", id);
                var dr = command.ExecuteReader();
                if (!dr.Read())
                {
                    throw new ArgumentException("Brak akcji danego strazaka");
                }
                while (dr.Read())
                {
                    var fa = new FireFigthersActions();
                    fa.IdAction = dr["IdAction"].ToString();
                    fa.StartTime = (DateTime)dr["StartTime"];
                    fa.EndTime = (DateTime)dr["EndTime"];
                    response.Add(fa);
                }
                dr.Close();
            }

            return response;
        }
       
    }
}
