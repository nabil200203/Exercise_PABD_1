using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inser_and_Get_Data
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi Ke Database\n");
                    Console.WriteLine("Masukkan User ID :");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password : ");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan database tujuan : ");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = DESKTOP\\QB0MM9G; " +
                                    "initial catalog = {0};" + 
                                    "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Keluar");
                                        Console.WriteLine("\nEnter your choice (1-3): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA TAMU\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA TAMU\n");
                                                    Console.WriteLine("Masukkan Nomor Induk Tamu : ");
                                                    string No_induk = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Tamu : ");
                                                    string Nama_tamu = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Alamat Tamu : ");
                                                    string alamat = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Kota : ");
                                                    string Kota = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Provinsi : ");
                                                    string provinsi = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nomor HP : ");
                                                    string no_hp = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Email : ");
                                                    string Email = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(No_induk, Nama_tamu, alamat, Kota, provinsi,no_hp,Email, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki " +
                                                            "akses untuk tambahan data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;

                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered");
                                    }

                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut\n");
                    Console.ResetColor();
                }
            }
        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * From dbo.tamu", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }
        public void insert(string No_Induk, string Nama_Tamu, string Almt, string kota, string provinsi, string no_hp, string email,
            SqlConnection con)
        {
            string str = "";
            str = "insert into dbo.Tamu (No_Induk, Nama_Tamu, Alamat, kota, provinsi, NO_HP, Email)"
                + " values (@No_Induk, @Nama_Tamu, @Alamat, @Kota, @provinsi,@NO_HP,@email)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("No_Induk", No_Induk));
            cmd.Parameters.Add(new SqlParameter("Nama_Tamu", Nama_Tamu));
            cmd.Parameters.Add(new SqlParameter("Alamat", Almt));
            cmd.Parameters.Add(new SqlParameter("Kota", kota));
            cmd.Parameters.Add(new SqlParameter("provinsi", provinsi));
            cmd.Parameters.Add(new SqlParameter("NO_HP", no_hp));
            cmd.Parameters.Add(new SqlParameter("email", email));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }
    }
}