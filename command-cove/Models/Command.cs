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
public class Command(
    int id,
    int commandSetId,
    string commandStr,
    string comment,
    string remark,
    DateTime creationTime,
    int sort)
{
    // 唯一标识
    public int Id { get; init; } = id;

    // 所属命令集ID
    public int CommandSetId { get; init; } = commandSetId;

    // 指令 str
    public string CommandStr { get; set; } = commandStr;

    // 注释
    public string Comment { get; set; } = comment;

    // 补充备注
    public string Remark { get; set; } = remark;

    // 创建时间
    public DateTime CreationTime { get; set; } = creationTime;

    // 在命令集中的顺序
    public int Sort { get; set; } = sort;
}