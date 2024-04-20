namespace FuDever.Configuration.Presentation.WebApi.Swagger;

public sealed class NSwagOption
{
    public DocOption Doc { get; set; } = new();

    public sealed class DocOption
    {
        public string Name { get; set; }

        public InfoOption Info { get; set; } = new();

        public sealed class InfoOption
        {
            public string Version { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public ContactOption Contact { get; set; } = new();

            public LicenseOption License { get; set; } = new();

            public sealed class ContactOption
            {
                public string Name { get; set; }

                public string Email { get; set; }
            }

            public sealed class LicenseOption
            {
                public string Name { get; set; }

                public string Url { get; set; }
            }
        }
    }
}
