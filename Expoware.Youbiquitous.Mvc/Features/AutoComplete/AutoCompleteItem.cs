///////////////////////////////////////////////////////////////////
//
// Youbiquitous.MVC v1.0
// Author: Dino Esposito
//


namespace Expoware.Youbiquitous.Mvc.Features.AutoComplete
{
    public class AutoCompleteItem
    {
        // Display text for the drop-down list (contains HTML styles)
        public string label { get; set; }

        // Unique ID of the returned item
        public string id { get; set; }

        // Value to copy in the textbox
        public string value { get; set; }
    }
}