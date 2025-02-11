using System;
using System.IO;
using VPG.Core;
using VPG.Core.Serialization;
using VPG.Editor.Configuration;
using UnityEditor;
using UnityEngine;

namespace VPG.Editor
{
    /// <summary>
    /// A static class that handles the course assets. It lets you to save, load, delete, and import training courses and provides multiple related utility methods.
    /// </summary>
    internal static class CourseAssetManager
    {
        /// <summary>
        /// Deletes the course with <paramref name="courseName"/>.
        /// </summary>
        internal static void Delete(string courseName)
        {
            if (CourseAssetUtils.DoesCourseAssetExist(courseName))
            {
                Directory.Delete(CourseAssetUtils.GetCourseAssetDirectory(courseName), true);
                AssetDatabase.Refresh();
            }
        }

        /// <summary>
        /// Imports the given <paramref name="course"/> by saving it to the proper directory. If there is a name collision, this course will be renamed.
        /// </summary>
        internal static void Import(ICourse course)
        {
            int counter = 0;
            string oldName = course.Data.Name;
            while (CourseAssetUtils.DoesCourseAssetExist(course.Data.Name))
            {
                if (counter > 0)
                {
                    course.Data.Name = course.Data.Name.Substring(0, course.Data.Name.Length - 2);
                }

                counter++;
                course.Data.Name += " " + counter;
            }

            if (oldName != course.Data.Name)
            {
                Debug.LogWarning($"We detected a name collision while importing course \"{oldName}\". We have renamed it to \"{course.Data.Name}\" before importing.");
            }

            Save(course);
        }

        /// <summary>
        /// Imports the course from file at given file <paramref name="path"/> if the file extensions matches the <paramref name="serializer"/>.
        /// </summary>
        internal static void Import(string path, ICourseSerializer serializer)
        {
            ICourse course;

            if (Path.GetExtension(path) != $".{serializer.FileFormat}")
            {
                Debug.LogError($"The file extension of {path} does not match the expected file extension of {serializer.FileFormat} of the current serializer.");
            }

            try
            {
                byte[] file = File.ReadAllBytes(path);
                course = serializer.CourseFromByteArray(file);
            }
            catch (Exception e)
            {
                Debug.LogError($"{e.GetType().Name} occured while trying to import file '{path}' with serializer '{serializer.GetType().Name}'\n\n{e.StackTrace}");
                return;
            }

            Import(course);
        }

        /// <summary>
        /// Save the <paramref name="course"/> to the file system.
        /// </summary>
        internal static void Save(ICourse course)
        {
            try
            {
                string path = CourseAssetUtils.GetCourseAssetPath(course.Data.Name);
                byte[] courseData = EditorConfigurator.Instance.Serializer.CourseToByteArray(course);
                WriteCourse(path, courseData);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        private static void WriteCourse(string path, byte[] courseData)
        {
            FileStream stream = null;
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                stream = File.Create(path);
                stream.Write(courseData, 0, courseData.Length);
                stream.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        /// <summary>
        /// Loads the course with the given <paramref name="courseName"/> from the file system and converts it into the <seealso cref="ICourse"/> instance.
        /// </summary>
        internal static ICourse Load(string courseName)
        {
            if (CourseAssetUtils.DoesCourseAssetExist(courseName))
            {
                string courseAssetPath = CourseAssetUtils.GetCourseAssetPath(courseName);
                byte[] courseBytes = File.ReadAllBytes(courseAssetPath);

                try
                {
                    return EditorConfigurator.Instance.Serializer.CourseFromByteArray(courseBytes);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Failed to load the course '{courseName}' from '{courseAssetPath}' because of: \n{ex.Message}");
                    Debug.LogError(ex);
                }
            }
            return null;
        }

        /// <summary>
        /// Renames the <paramref name="course"/> to the <paramref name="newName"/> and moves it to the appropriate directory. Check if you can rename before with the <seealso cref="CanRename"/> method.
        /// </summary>
        internal static void RenameCourse(ICourse course, string newName)
        {
            if (CourseAssetUtils.CanRename(course, newName, out string errorMessage) == false)
            {
                Debug.LogError($"Course {course.Data.Name} was not renamed because:\n\n{errorMessage}");
                return;
            }

            string oldDirectory = CourseAssetUtils.GetCourseAssetDirectory(course.Data.Name);
            string newDirectory = CourseAssetUtils.GetCourseAssetDirectory(newName);

            Directory.Move(oldDirectory, newDirectory);
            File.Move($"{oldDirectory}.meta", $"{newDirectory}.meta");

            string newAsset = CourseAssetUtils.GetCourseAssetPath(newName);
            string oldAsset = $"{CourseAssetUtils.GetCourseAssetDirectory(newName)}/{course.Data.Name}.{EditorConfigurator.Instance.Serializer.FileFormat}";
            File.Move(oldAsset, newAsset);
            File.Move($"{oldAsset}.meta", $"{newAsset}.meta");
            course.Data.Name = newName;

            Save(course);
        }
    }
}
