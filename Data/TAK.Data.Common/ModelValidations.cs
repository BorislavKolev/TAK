namespace TAK.Data.Common
{
    public class ModelValidations
    {
        public static class Location
        {
            public const string EmptyNameError = "Please enter location name.";

            public const string EmptyDescriptionError = "Please enter location description";

            public const string EmptyPicturesError = "Please add at least one picture.";

            public const string EmptyTypeError = "Please choose a location type.";

            public const string EmptyMapLinkError = "Please add location map link.";

            public const string EmptyPerksError = "Please enter at least one perk";
        }

        public static class NewsPost
        {
            public const string EmptyTitleError = "Please enter news post title.";

            public const string EmptyContentError = "Please enter news post content";

            public const string EmptyPicturesError = "Please add at least one picture.";
        }
    }
}
