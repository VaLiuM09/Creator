﻿using System.Collections.Generic;

namespace VPG.CreatorEditor.CourseValidation
{
    /// <summary>
    /// ValidationScope limits the scope of the validation done. For example scopes are:
    /// * Course <see cref="CourseValidationScope"/>
    /// * Chapter <see cref="ChapterValidationScope"/>
    /// * Step <see cref="StepValidationScope"/>
    /// </summary>
    internal interface IValidationScope
    {
        /// <summary>
        /// Returns true if the object given can be validated by this scope.
        /// </summary>
        bool CanValidate(object entityObject);

        /// <summary>
        /// Validates the given <paramref name="entityObject"/> in a specific <paramref name="context"/>. The object has to be able to be validated.
        /// </summary>
        /// <param name="entityObject">Object which is the target of the validation.</param>
        /// <param name="context">Context this validation runs in, has to be the correct one.</param>
        /// <returns>List of reports regarding invalid objects related to the <paramref name="entityObject"/>.</returns>
        List<EditorReportEntry> Validate(object entityObject, IContext context);
    }
}
