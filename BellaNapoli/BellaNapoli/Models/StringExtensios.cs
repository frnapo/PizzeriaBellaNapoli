namespace BellaNapoli.Models
{
    public static class StringExtensios
    {
        public static (string badgeColor, string textColor) GetBadgeColors(this string ingrediente)
        {
            switch (ingrediente.Trim())
            {
                case "Pomodoro":
                    return ("red", "#ffffff");
                case "Aglio":
                    return ("#C9CDCC", "#212529");
                case "Mozzarella":
                    return ("#ECECE3", "#212529");
                case "Basilico":
                    return ("#008000", "#ffffff");
                case "Peperoni":
                    return ("#ff4500", "#ffffff");
                case "Acciughe":
                    return ("#708090", "#ffffff");
                case "Funghi":
                    return ("#8B4513", "#ffffff");
                case "Cipolla":
                    return ("#D3D3D3", "#212529");
                case "Prosciutto":
                    return ("#FFB6C1", "#212529");
                case "Salsiccia":
                    return ("#A52A2A", "#ffffff");
                case "Olive":
                    return ("#000000", "#ffffff");
                case "Capperi":
                    return ("#FFFF00", "#212529");
                case "Rucola":
                    return ("#556B2F", "#ffffff");
                case "Gorgonzola":
                    return ("#6495ED", "#ffffff");
                case "Parmigiano":
                    return ("#F5DEB3", "#212529");
                case "Peperoncino":
                    return ("#FF0000", "#ffffff");
                case "Origano":
                    return ("#A0522D", "#ffffff");
                case "Wurstel":
                    return ("#FFC0CB", "#212529");
                case "Patatine":
                    return ("gold", "#212529");
                default:
                    return ("#f8f9fa", "#212529");
            }
        }
    }
}