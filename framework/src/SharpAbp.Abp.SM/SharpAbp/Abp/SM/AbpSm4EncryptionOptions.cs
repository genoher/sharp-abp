﻿namespace SharpAbp.Abp.SM
{
    public class AbpSm4EncryptionOptions
    {
        public byte[] DefaultIv { get; set; }
        public string DefaultMode { get; set; }
        public string DefaultPadding { get; set; }
    }
}
