using CommunityToolkit.Mvvm.ComponentModel;

namespace JR.CodeGenerator.Models
{
    /// <summary>
    /// TableViewDto
    /// </summary>
    /// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableObject" />
    public partial class TableView : ObservableObject
    {
        /// <summary>
        /// The is selected
        /// </summary>
        /// TODO Edit XML Comment Template for isSelected
        private bool isSelected;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableView" /> class.
        /// </summary>
        public TableView()
        {
            Children = new List<TableView>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        /// TODO Edit XML Comment Template for Name
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the schema.
        /// </summary>
        /// <value>
        /// The schema.
        /// </value>
        /// TODO Edit XML Comment Template for Schema
        public string Schema { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string FullName { get => String.IsNullOrWhiteSpace(Schema) ? Name : $"{Schema}.{Name}"; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    if (Name == "Tablas" || Name == "Vistas")
                    {
                        foreach (var item in Children)
                        {
                            item.IsSelected = value;
                        }
                    }
                }

                SetProperty(ref isSelected, value);

            }
        }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public List<TableView> Children { get; set; }

        /// <summary>
        /// Gets or sets the image URI.
        /// </summary>
        /// <value>
        /// The image URI.
        /// </value>
        public string ImageUri { get; set; }

        /// <summary>
        /// Tos the string.
        /// </summary>
        /// <returns>
        /// A string.
        /// </returns>
        public override string ToString()
        {
            return (FullName == "Tablas" || FullName == "Vistas") ? $"{Name} ({Children.Count})" : FullName;
        }
    }
}
