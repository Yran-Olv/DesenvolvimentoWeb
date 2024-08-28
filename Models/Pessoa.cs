public class Pessoa
{
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public float Peso { get; set; }
    public float Altura { get; set; }

    public float CalcularIMC()
    {
        return Peso / (Altura * Altura);
    }
}
