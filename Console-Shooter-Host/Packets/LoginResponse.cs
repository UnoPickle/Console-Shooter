﻿namespace Console_Shooter_Host.Packets;

public class LoginResponse(uint status)
{
    public const uint Successful = 1;
    public const uint Unsuccessful = 0;

    public uint status = status;
}