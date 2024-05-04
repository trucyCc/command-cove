using command_cove.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class DatabaseManager : DbContext
{
    private string _connectionString = "Data Source=command_cove.db";

    public DbSet<Folder> Folders { get; set; }

    public DbSet<CommandSet> CommandSets { get; set; }

    public DbSet<Command> Commands { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        InitializeDatabase();
        options.UseSqlite(_connectionString);
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);

        connection.Open();

        // 创建表
        var createTableCommand = connection.CreateCommand();
        createTableCommand.CommandText = @"
                CREATE TABLE IF NOT EXISTS Folders (
                    Id  INTEGER PRIMARY KEY,
                    name TEXT NOT NULL,
                    parentId INTEGER,
                    type INTEGER,
                    creationTime  DATETIME
                )";
        createTableCommand.ExecuteNonQuery();
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Folder>(entity =>
    //     {
    //         entity.ToTable("Folders"); // 指定表名
    //         entity.HasKey(e => e.Id); // 指定主键
    //         entity.Property(e => e.Name).IsRequired(); // 指定 Name 为必需字段
    //     });
    //
    //     modelBuilder.Entity<CommandSet>(entity =>
    //     {
    //         entity.ToTable("CommandSets"); // 指定表名
    //         entity.HasKey(e => e.Id); // 指定主键
    //         entity.Property(e => e.Name).IsRequired(); // 指定 Name 为必需字段
    //     });
    //
    //     modelBuilder.Entity<Command>(entity =>
    //     {
    //         entity.ToTable("Commands"); // 指定表名
    //         entity.HasKey(e => e.Id); // 指定主键
    //     });
    // }
}