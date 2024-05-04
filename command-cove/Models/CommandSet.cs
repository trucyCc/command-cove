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
public class CommandSet
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 所属目录ID
    /// </summary>
    public int FolderId { get; set; }

    /// <summary>
    /// 命令集名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; set; }
}