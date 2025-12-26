using System.Runtime.CompilerServices;
using Hamkare.Common.Interface.Entities;

namespace Hamkare.Common.Entities;

public static class EntityExtension
{
    public static TRelated Load<TRelated>(
        this Action<object, string> loader,
        object entity,
        ref TRelated navigationField,
        [CallerMemberName] string navigationName = null)
        where TRelated : class
    {
        loader?.Invoke(entity, navigationName);

        return navigationField;
    }

    #region Get All

    public static List<T> GetAll<T>(this IEnumerable<T> list) where T : IRootEntity
    {
        return list?.Where(x => !x.Deleted).ToList() ?? [];
    }

    public static List<T> GetAll<T>(this IEnumerable<T> list, Func<T, bool> predicate) where T : IRootEntity
    {
        return list?.Where(x => !x.Deleted).Where(predicate).ToList() ?? [];
    }

    public static List<T> GetAllActive<T>(this IEnumerable<T> list, Func<T, bool> predicate) where T : IRootEntity
    {
        return list?.Where(x => x.Active && !x.Deleted).Where(predicate).ToList() ?? [];
    }

    public static List<T> GetAllActive<T>(this IEnumerable<T> list, Func<T, bool> predicate, int take, int skip)
        where T : IRootEntity
    {
        return list?.Where(x => x.Active && !x.Deleted).Where(predicate).Take(take).Skip(skip).ToList() ?? [];
    }

    public static List<T> GetAllActive<T>(this IEnumerable<T> list, int take, int skip) where T : IRootEntity
    {
        return list?.Where(x => x.Active && !x.Deleted).Take(take).Skip(skip).ToList() ?? [];
    }

    public static List<T> GetAllActive<T>(this IEnumerable<T> list) where T : IRootEntity
    {
        return list?.Where(x => x.Active && !x.Deleted).ToList() ?? [];
    }

    #endregion

    #region Get

    public static T Get<T>(this IEnumerable<T> list, long id) where T : IRootEntity
    {
        return list.FirstOrDefault(x => x.Id == id && !x.Deleted);
    }

    public static T Get<T>(this IEnumerable<T> list) where T : IRootEntity
    {
        return list.FirstOrDefault(x => !x.Deleted);
    }

    public static T Get<T>(this IEnumerable<T> list, Func<T, bool> predicate) where T : IRootEntity
    {
        return list.Where(x => !x.Deleted).FirstOrDefault(predicate);
    }

    public static T GetActive<T>(this IEnumerable<T> list, long id) where T : IRootEntity
    {
        return list.FirstOrDefault(x => x.Id == id && x.Active && !x.Deleted);
    }

    public static T GetActive<T>(this IEnumerable<T> list) where T : IRootEntity
    {
        return list.FirstOrDefault(x => x.Active && !x.Deleted);
    }

    public static T GetActive<T>(this IEnumerable<T> list, Func<T, bool> predicate) where T : IRootEntity
    {
        return list.Where(x => x.Active && !x.Deleted).FirstOrDefault(predicate);
    }

    #endregion

    #region Any

    public static bool GetAny<T>(this IEnumerable<T> list) where T : IRootEntity
    {
        return list.Any(x => !x.Deleted);
    }

    public static bool GetAny<T>(this IEnumerable<T> list, Func<T, bool> predicate) where T : IRootEntity
    {
        return list.Where(x => !x.Deleted).Where(predicate).Any();
    }

    public static bool GetAnyActive<T>(this IEnumerable<T> list) where T : IRootEntity
    {
        return list.Any(x => x.Active && !x.Deleted);
    }

    public static bool GetAnyActive<T>(this IEnumerable<T> list, Func<T, bool> predicate) where T : IRootEntity
    {
        return list.Where(x => x.Active && !x.Deleted).Any(predicate);
    }

    #endregion

    #region Count

    public static int GetCount<T>(this IEnumerable<T> list) where T : IRootEntity
    {
        return list.Count(x => !x.Deleted);
    }

    public static int GetCount<T>(this IEnumerable<T> list, Func<T, bool> predicate) where T : IRootEntity
    {
        return list.Where(x => !x.Deleted).Where(predicate).Count();
    }

    public static int GetActiveCount<T>(this IEnumerable<T> list) where T : IRootEntity
    {
        return list.Count(x => x.Active && !x.Deleted);
    }

    public static int GetActiveCount<T>(this IEnumerable<T> list, Func<T, bool> predicate) where T : IRootEntity
    {
        return list.Where(x => x.Active && !x.Deleted).Count(predicate);
    }

    #endregion

    #region Group

    public static List<long> GetChildId<T>(this IEnumerable<ICategoryEntity<T>> model, long parentId)
    {
        var groupEntities = model.ToList();

        var child = groupEntities.GetAll(x => x.ParentId == parentId);
        var result = child.Select(x => x.Id).ToList();

        foreach (var item in child)
            result.AddRange(GetChildId(groupEntities, item.Id));

        return result;
    }

    public static List<ICategoryEntity<T>> AvailableParent<T>(this IEnumerable<ICategoryEntity<T>> model, long groupId)
    {
        var groupEntities = model.ToList();
        
        var child = GetChildId(groupEntities, groupId);

        return groupEntities.GetAll(x => !child.Contains(x.Id) && x.Id != groupId);
    }

    #endregion
}