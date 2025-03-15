namespace cslox;

// From https://man.freebsd.org/cgi/man.cgi?query=sysexits&apropos=0&sektion=0&manpath=FreeBSD+4.3-RELEASE&format=html
public enum SysExits
{
    Success = 0,
    Usage = 64,
    DataError = 65,
    NoInput = 66,
    NoUser = 67,
    NoHost = 68,
    Unavailable = 69,
    Software = 70,
    OsError = 71,
    OsFile = 72,
    CantCreate = 73,
    IoError = 74,
    TempFail = 75,
    Protocol = 76,
    NoPerm = 77,
    Config = 78,
}
