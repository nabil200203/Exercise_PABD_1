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
                                string strKoneksi = "Data source = DESKTOP-QB0MM9G; " +
                                    "initial catalog = HOTEL; " + "User ID = admin01; password = 123";
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
                                                    Console.WriteLine("DATA MAHASISWA\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA MAHASISWA\n");
                                                    Console.WriteLine("Masukkan NIM : ");
                                                    string NIM = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Namah Mahasiswa : ");
                                                    string NmaMhs = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Alamat Mahasiswa : ");
                                                    string Almt = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Jenis Kelamin (L/P)");
                                                    string jk = Console.ReadLine();
                                                    Console.WriteLine("Masukkan No Telpon :");
                                                    string notlp = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(NIM, NmaMhs, Almt, jk, notlp, conn);
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
            SqlCommand cmd = new SqlCommand("Select * From HRD.Mahasiswa", con);
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
        public void insert(string NIM, string NmaMhs, string Almt, string jk, string notlpn,
            SqlConnection con)
        {
            string str = "";
            str = "insert into HRD.MAHASISWA (NIM, NamaMhs, AlamatMhs, Sex, PhoneMhs)"
                + " values (@nim, @nma, @alamat, @JK, @Phn)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("nim", NIM));
            cmd.Parameters.Add(new SqlParameter("nma", NmaMhs));
            cmd.Parameters.Add(new SqlParameter("alamat", Almt));
            cmd.Parameters.Add(new SqlParameter("JK", jk));
            cmd.Parameters.Add(new SqlParameter("Phn", notlpn));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }
    }
}