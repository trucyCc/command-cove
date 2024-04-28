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
    public int Id { get; set; } = id;

    public int FolderId { get; set; } = folderId;

    public string Name { get; set; } = name;

    // 创建时间
    public DateTime CreationTime { get; set; } = creationTime;
}