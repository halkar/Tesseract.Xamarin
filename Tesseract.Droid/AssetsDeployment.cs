using System;

namespace Tesseract.Droid
{
    /// <summary>
    /// Flag showing how often tessdata assets are deployed.
    /// </summary>
    public enum AssetsDeployment
    {
        /// <summary>
        /// Assets are deployed once per version.
        /// Higher performance but tessdata is not updated if application version is not updated.
        /// Recomended for release versions.
        /// </summary>
        OncePerVersion,
        /// <summary>
        /// Tessdata is updated each time Init method is called.
        /// Lower perfomance.
        /// Recomended for debug versions.
        /// </summary>
        OncePerInitialization
    }
}

