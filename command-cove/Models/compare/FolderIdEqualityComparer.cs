using System.Collections.Generic;

namespace command_cove.Models.compare;

public class FolderIdEqualityComparer: IEqualityComparer<Folder>
{
    public bool Equals(Folder x, Folder y)
    {
        // 如果两个对象都为 null，则认为它们相等
        if (x == null && y == null)
            return true;
        // 如果其中一个对象为 null，则认为它们不相等
        if (x == null || y == null)
            return false;
        // 比较两个对象的 Id 属性
        return x.Id == y.Id;
    }

    public int GetHashCode(Folder obj)
    {
        // 返回对象的 Id 属性的哈希码
        return obj.Id.GetHashCode();
    }
}