using System.Runtime.CompilerServices;
using System.Threading;

public class Configurations
{
    private bool soundMuted;
    private static Configurations configurations;

    private Configurations()
    {
        this.soundMuted = false;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public static Configurations getInstance()
    {
        if (configurations == null)
            configurations = new Configurations();

        return configurations;
    }

    public bool isSoundMuted()
    {
        return soundMuted;
    }

    public void setSoundMuted(bool soundMuted)
    {
        this.soundMuted = soundMuted;
    }
}