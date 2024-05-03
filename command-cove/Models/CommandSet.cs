using System;

namespace command_cove.Models;

/*
 *
 * 命令集
 *
 * @Description:
 * @Date: 2024年04月27日 星期六 17:17:32
 * @Author: Trucy
 * @Modify:
 */
public class CommandSet(int id, int folderId, string name, DateTime creationTime)
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    public int Id { get; set; } = id;

    /// <summary>
    /// 所属目录ID
    /// </summary>
    public int FolderId { get; set; } = folderId;

    /// <summary>
    /// 命令集名称
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; set; } = creationTime;
}