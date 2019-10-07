using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System.Text;
using System;

public class AzureConnection : MonoBehaviour
{
    SqlConnectionStringBuilder builder;
    SqlConnection connection;
    SqlCommand command;
    SqlDataReader reader;

    Text nick;
    Text pass;
    GameObject error;

    // Start is called before the first frame update
    void Start()
    {
        builder = new SqlConnectionStringBuilder();
        builder.DataSource = "xxxxxxx";
        builder.UserID = "xxxxxx";
        builder.Password = "xxxxxxx";
        builder.InitialCatalog = "xxxxxxx";
        connection = new SqlConnection(builder.ConnectionString);

        nick = GameObject.Find("NickText").GetComponent<Text>();
        pass = GameObject.Find("PassText").GetComponent<Text>();
        error = GameObject.Find("Error");
        error.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Inserisci()
    {
        if (Presente() == false)
        {
            connection.Open();
            StringBuilder sb = new StringBuilder();
            String query = "INSERT INTO USERS (nickname, pass) VALUES ('" + nick.text + "', '" + pass.text + "')";
            sb.Append(query);
            String sql = sb.ToString();
            command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        else
        {
            error.SetActive(true);
        }
    }

    bool Presente()
    {
        connection.Open();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT pass fROM utenti WHERE nome = '" + nick.text + "'");
        String sql = sb.ToString();
        command = new SqlCommand(sql, connection);
        reader = command.ExecuteReader();
        bool esiste = false;
        while (reader.Read())
        {
            esiste = true;
        }
        connection.Close();
        return esiste;
    }

}
