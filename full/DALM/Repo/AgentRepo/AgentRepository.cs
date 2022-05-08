using Dapper;
using MManager.Agent;
using System.Data.SQLite;

namespace MManager.Repo
{
    public class AgentRepository : IAgentRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;";



        public AgentModel GetAgentById(long id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<AgentModel>("SELECT * FROM Agents WHERE AgentId = @agentId",
                    new
                    {
                        agentId = id
                    });
            }
        }


        public List<AgentModel> GetAllAgents()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<AgentModel>("SELECT * FROM Agents").ToList();
            }
        }


        public void SetNewAgent(AgentModel item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO Agents (AgentUrl) VALUES(@value)",
                new
                {
                    value = item.AgentUrl,
                });
            }
        }
    }
}
