﻿using System.ComponentModel;

#nullable disable

namespace ShootRunner
{
    public class BackgroundJob
    {
        public long starttime = 0;
        public int maxEcecutionTime = 5; // seconds
        public CancellationTokenSource cts = null;
        public BackgroundWorker bw = null;
        public CancellationToken token = CancellationToken.None;
    }
}
