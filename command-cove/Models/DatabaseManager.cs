using command_cove.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

/*
 *
 * Sqlite数据管理
 *
 * @Description:
 * @Date: 2024年05月04日 星期六 12:01:58
 * @Author: Trucy
 * @Modify:
 */
public class DatabaseManager : DbContext
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    private readonly string _connectionString = "Data Source=command_cove.db";

    /// <summary>
    /// 文件夹
    /// </summary>
    public DbSet<Folder> Folders { get; set; }

    /// <summary>
    /// 命令
    /// </summary>
    public DbSet<Command> Commands { get; set; }

    /// <summary>
    /// 初始配置，设置sqlite连接
    /// </summary>
    /// <param name="options"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // 初始化表结构
        InitializeDatabase();
        options.UseSqlite(_connectionString);
    }

    /// <summary>
    /// 程序启动后初始化表结构
    /// </summary>
    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // 文件夹
        var createTableCommand = connection.CreateCommand();
        createTableCommand.CommandText = @"
                CREATE TABLE IF NOT EXISTS Folders (
                    Id  INTEGER PRIMARY KEY,
                    name TEXT NOT NULL,
                    parentId INTEGER,
                    type INTEGER,
                    creationTime  DATETIME
                )";
        // 执行语句
        createTableCommand.ExecuteNonQuery();
        
        // 命令
        createTableCommand.CommandText = @"
                CREATE TABLE IF NOT EXISTS Commands (
                    id INTEGER PRIMARY KEY,
                    commandSetId INTEGER,
                    commandStr TEXT,
                    comment TEXT,
                    remark TEXT,
                    isDelete BOOLEAN,
                    creationTime DATETIME,
                    deleteTime DATETIME,
                    sort INTEGER
                )";
        createTableCommand.ExecuteNonQuery();
    }
}