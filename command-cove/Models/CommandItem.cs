using System;

namespace command_cove.Models;

// 单个指令
public class CommandItem
{
    // 命令
    public string Command { get; set; }

    // 备注
    public string Remark { get; set; }

    // 创建时间
    public DateTime CreationTime { get; set; }

    // 顺序
    public int Sort { get; set; }
}