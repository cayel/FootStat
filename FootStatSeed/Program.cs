using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootStatSeed
{
    
    public class Competition
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
    class Program
    {
        static int extractGoalHome(string score)
        {
            Char delimiter = '-';
            String[] goals = score.Split(delimiter);
            return Int32.Parse(goals[0]);
        }
        static int extractGoalAway(string score)
        {
            Char delimiter = '-';
            String[] goals = score.Split(delimiter);
            return Int32.Parse(goals[1]);
        }
        static bool validCompetition(string competitionYear)
        {
            int year = Int32.Parse(competitionYear);
            
            return ((year > 1990) && (year < 2019));
        }

        static bool linkedCompetitionAndTeam(int competitionId, int teamId)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                string sql = "SELECT idCompetition, idTeam From CompetitionTeam where idCompetition=" + competitionId.ToString()+" And IdTeam=" + teamId.ToString();

                // Create command.
                SqlCommand cmd = new SqlCommand();

                // Set connection for Command.
                cmd.Connection = conn;
                cmd.CommandText = sql;
                conn.Open();
                object competitionTeam = cmd.ExecuteScalar();
                return (competitionTeam != null);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                return false;
            }
            finally
            {
                // Close connection.
                conn.Close();
                // Dispose object, freeing Resources.
                conn.Dispose();
            }
        }

        static int AddFixture(int competitionId, int matchDay, DateTime dateMatch, int teamHome, int teamAway, int goalHome, int goalAway)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string sqlInsert = "INSERT INTO Fixture (idCompetition, idTeamHome, idTeamAway, matchDay, matchDate, goalsHome, goalsAway) "+
                    "VALUES (@competitionId, @teamHome, @teamAway, @matchDay, @matchDate, @goalsHome, @goalsAway); ";
                cmd.Connection = conn;
                cmd.CommandText = sqlInsert;
                cmd.Parameters.Add("@competitionId", SqlDbType.Int);
                cmd.Parameters["@competitionId"].Value = competitionId;
                cmd.Parameters.Add("@teamHome", SqlDbType.Int);
                cmd.Parameters["@teamHome"].Value = teamHome;
                cmd.Parameters.Add("@teamAway", SqlDbType.Int);
                cmd.Parameters["@teamAway"].Value = teamAway;
                cmd.Parameters.Add("@goalsHome", SqlDbType.Int);
                cmd.Parameters["@goalsHome"].Value = goalHome;
                cmd.Parameters.Add("@goalsAway", SqlDbType.Int);
                cmd.Parameters["@goalsAway"].Value = goalAway;
                cmd.Parameters.Add("@matchDay", SqlDbType.Int);
                cmd.Parameters["@matchDay"].Value = matchDay;
                cmd.Parameters.Add("@matchDate", SqlDbType.Date);
                cmd.Parameters["@matchDate"].Value = dateMatch;
                conn.Open();
                cmd.ExecuteScalar();
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                return -1;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        static int linkCompetitionAndTeam(int competitionId, int teamId)
        {
            if (linkedCompetitionAndTeam(competitionId, teamId)) return 0;

            SqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string sqlInsert = "INSERT INTO CompetitionTeam (idCompetition, idTeam) VALUES (@competitionId, @teamId); ";
                cmd.Connection = conn;
                cmd.CommandText = sqlInsert;
                cmd.Parameters.Add("@competitionId", SqlDbType.Int);
                cmd.Parameters["@competitionId"].Value = competitionId;
                cmd.Parameters.Add("@teamId", SqlDbType.Int);
                cmd.Parameters["@teamId"].Value = teamId;
                conn.Open();
                cmd.ExecuteScalar();
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                return -1;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        static int createTeam(string team)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string sqlInsert = "INSERT INTO Team (name) VALUES (@teamName); "
                    + "SELECT CAST(scope_identity() AS int)";
                cmd.Connection = conn;
                cmd.CommandText = sqlInsert;
                cmd.Parameters.Add("@teamName", SqlDbType.VarChar);
                cmd.Parameters["@teamName"].Value = team;
                Int32 newTeamId = -1;
                conn.Open();
                newTeamId = (Int32)cmd.ExecuteScalar();
                return (int)newTeamId;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                return -1;
            }
            finally
            {
                // Close connection.
                conn.Close();
                // Dispose object, freeing Resources.
                conn.Dispose();
            }
        }

        static int competitionTeamsCount(int idCompetition)
        {
            object objectCount;
            SqlConnection conn = DBUtils.GetDBConnection();

            try
            {
                string sql = "SELECT count(*) From CompetitionTeam where idCompetition=" + idCompetition.ToString();

                // Create command.
                SqlCommand cmd = new SqlCommand();

                // Set connection for Command.
                cmd.Connection = conn;
                cmd.CommandText = sql;
                conn.Open();
                objectCount = cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                return -1;
            }
            finally
            {
                // Close connection.
                conn.Close();
                // Dispose object, freeing Resources.
                conn.Dispose();
            }
            if (objectCount != null) return (int)objectCount;
            else return 0;
        }
        static int createAndGetTeam(string team)
        {
            object teamId;
            SqlConnection conn = DBUtils.GetDBConnection();

            try
            {
                string sql = "SELECT id From Team where name='" + team + "'";

                // Create command.
                SqlCommand cmd = new SqlCommand();

                // Set connection for Command.
                cmd.Connection = conn;
                cmd.CommandText = sql;
                conn.Open();
                teamId = cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                return -1;
            }
            finally
            {
                // Close connection.
                conn.Close();
                // Dispose object, freeing Resources.
                conn.Dispose();
            }
            if (teamId != null) return (int)teamId;
            else return createTeam(team);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Importateur de résultats sportifs");
            Console.WriteLine("=================================");
            //string csv_file_path = @"C:\Users\AYEL\Documents\Dev\foot-data-extract\data\liga-94.csv";

            Console.Write("Fichier à importer : ");
            string fileName = Console.ReadLine();

            DataTable csvData = GetDataTabletFromCSVFile(fileName);

            Console.WriteLine("Lignes de données trouvées :" + csvData.Rows.Count);

            if (csvData.Rows.Count > 0 )
            {
                Console.Write("Identifiant de la compétition : ");
                int competitionId = Int32.Parse(Console.ReadLine());
                Console.Write("Nombre de matchs par journée : ");
                int matchCount = Int32.Parse(Console.ReadLine());
                try
                {
                    int countFixture = 1;
                    int matchDay = 1;

                    foreach (DataRow row in csvData.Rows)
                    {                         
                        DateTime dateMatch = Convert.ToDateTime(row["Date"].ToString());
                        string team1 = row["Team 1"].ToString();
                        string team2 = row["Team 2"].ToString();
                        string score = row["FT"].ToString();
                        int teamHome = createAndGetTeam(team1);
                        linkCompetitionAndTeam(competitionId, teamHome);
                        int teamAway = createAndGetTeam(team2);
                        linkCompetitionAndTeam(competitionId, teamAway);
                        int goalHome = extractGoalHome(score);
                        int goalAway = extractGoalAway(score);
                        AddFixture(competitionId, matchDay, dateMatch, teamHome, teamAway, goalHome, goalAway);
                        countFixture++;
                        if ((countFixture % matchCount) == 1) matchDay++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                    Console.WriteLine(e.StackTrace);
                }
            }
            Console.WriteLine();
            Console.Write("Appuyez une touche pour sortir...");
            Console.ReadLine();
        }

        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)

        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;

                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }

                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }        
            return csvData;
        }
    }
}
