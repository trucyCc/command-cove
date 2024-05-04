using System;

namespace command_cove.Models;

/*
 *
 * 命令行
 *
 * @Description:
 * @Date: 2024年04月27日 星期六 17:17:20
 * @Author: Trucy
 * @Modify:
 */
public class Command
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// 所属命令集ID
    /// </summary>
    public int CommandSetId { get; init; }

    /// <summary>
    /// 指令 str
    /// </summary>
    public string CommandStr { get; set; }

    /// <summary>
    /// 注释
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// 补充备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// 在命令集中的顺序
    /// </summary>
    public int Sort { get; set; }
}