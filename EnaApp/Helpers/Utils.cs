namespace EnaApp.Helpers
{
    public static class Utils
    {

    }

    /*
     * After careful sdk study have found MonoDroid integrated extension for this purpose:
    public static TResult JavaCast<TResult>(this Android.Runtime.IJavaObject instance)
    where TResult : class, Android.Runtime.IJavaObject
    Member of Android.Runtime.Extensions
    https://stackoverflow.com/questions/6594250/type-cast-from-java-lang-object-to-native-clr-type-in-monodroid
     */
    public static class ObjectTypeHelper
    {
        public static T Cast<T>(this Java.Lang.Object obj) where T : class
        {
            var propertyInfo = obj.GetType().GetProperty("Instance");
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
        }
    }
}