using CrudApp.Data.Helpers;
using CrudApp.Data.Service.Abstract;
using CrudApp.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApp.Data.Service
{
    public class Dapper_BookService : IBookService
    {
        public async Task<bool> Add(Book model)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    await connection.QueryAsync<Book>("sp_BookSave",
                                this.SetParameters(model),
                                commandType: CommandType.StoredProcedure);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> AddList(List<Book> modelList)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    await connection.ExecuteAsync(@"INSERT INTO BOOK(Title, Author) VALUES (@Title, @Author)", modelList.ToArray());
                   
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<Book>> Search(int page, int pageSize)
        {
            page = page <= 0 ? 0 : page - 1;
      
            int offset = page * pageSize;
            try
            {
                using (IDbConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    var books = await connection.QueryAsync<Book>($"SELECT * FROM BOOK ORDER BY Id OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY");

                    if (books != null && books.Count() > 0)
                    {
                        return books.ToList();
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Book> GetById(int id)
        {
            try
            {

                using (IDbConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    var book = await connection.QueryAsync<Book>($"SELECT * FROM BOOK WHERE Id = {id}");


                    if (book != null && book.Count() > 0)
                    {
                        return book.FirstOrDefault();
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    await connection.QueryAsync($"DELETE BOOK WHERE Id = {id}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> Update(int id, Book model)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(SqlHelper.ConnectionString))
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    var sqlStatement = @"UPDATE BOOK SET Title = @Title, Author = @Author WHERE Id = "+id;
                    await connection.ExecuteAsync(sqlStatement, model);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        private DynamicParameters SetParameters(Book book)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Title", book.Title);
            parameters.Add("@Author", book.Author);

            return parameters;

        }
    }

}
