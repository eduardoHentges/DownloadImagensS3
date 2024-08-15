using System.Collections.Generic;

public class Product
{
    public string codigo { get; set; }
    public string id { get; set; }
    public string imagem { get; set; }
    public string nome { get; set; }
    public List<ImagemAdicional> imagensAdicionais { get; set; } // Lista de URLs para imagens adicionais
    

}

public class ImagemAdicional
{
    public string imagem { get; set; } // Rename this property to match the JSON response
}