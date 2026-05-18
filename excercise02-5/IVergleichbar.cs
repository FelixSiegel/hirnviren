namespace CampusLeihsystem;

public interface IVergleichbar<T>
{
    int VergleicheMit(T anderer);
    bool IstGroesserAls(T anderer);
    bool IstKleinerAls(T anderer);
}