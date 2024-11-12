using System.ComponentModel;
using System.Timers;

#nullable disable


namespace ShootRunner
{
    /// <summary>
    /// repository for thread related functions</summary>
    public class Job //UID8736422044
    {

        public static List<BackgroundJob> backgroundJob = new List<BackgroundJob>();

        private static System.Timers.Timer timer = null;

        /// <summary>
        /// run task in thread </summary>
        /// <example> 
        /// This of use doJob method
        /// <code>
        /// Job.DoJob(
        ///    new DoWorkEventHandler(
        ///        delegate (object o, DoWorkEventArgs args)
        ///        {
        ///            // run in new thread
        ///        }
        ///    ),
        ///    new RunWorkerCompletedEventHandler(
        ///        delegate (object o, RunWorkerCompletedEventArgs args)
        ///        {
        ///            // complete
        ///        }
        ///    )
        /// );
        /// </code>
        /// </example>
        public static void DoJob(DoWorkEventHandler doJob = null, RunWorkerCompletedEventHandler afterJob = null, int maxEcecutionTime = 60)
        {
            if (timer == null && maxEcecutionTime != 0) {
                timer = new System.Timers.Timer(1000); // Set interval to 1000ms (1 second)
                timer.Elapsed += TimerElapsed;
                timer.AutoReset = true; // Ensures the timer runs continuously
                timer.Start();
            }
          
            try
            {
                BackgroundJob job = new BackgroundJob();
                backgroundJob.Add(job);

                job.cts = new CancellationTokenSource();
                job.token = job.cts.Token;

                
                job.maxEcecutionTime = maxEcecutionTime;
                job.starttime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                job.bw = new BackgroundWorker
                {
                    WorkerSupportsCancellation = true
                };
                job.bw.WorkerReportsProgress = true;
                job.bw.DoWork += doJob;
                job.bw.RunWorkerCompleted += afterJob;
                job.bw.RunWorkerCompleted += CompleteJob;
                job.bw.RunWorkerAsync(job);
                
            }
            catch (Exception ex)
            {
                Program.error("get link name error: " + ex.Message);
            }
        }


        public static void CompleteJob(object sender, RunWorkerCompletedEventArgs e) {
            try
            {
                BackgroundWorker bw = sender as BackgroundWorker;
                BackgroundJob toremove = null;
                foreach (BackgroundJob job in backgroundJob)
                {
                    if (job.bw == bw) {
                        toremove = job;
                        break;
                    }
                }

                if (toremove != null)
                {
                    backgroundJob.Remove(toremove);
                }
            }
            catch (Exception ex)
            {

                Program.error(ex.Message);
            }

        }

        private static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            List<BackgroundJob> backgroundJobToRemove = new List<BackgroundJob>();

            long time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            foreach (BackgroundJob job in backgroundJob)
            {
                
                if (job.maxEcecutionTime != 0 && (time - job.starttime > job.maxEcecutionTime))
                {
                    backgroundJobToRemove.Add(job);
                }
            }

            foreach (BackgroundJob job in backgroundJobToRemove) {
                job.cts.Cancel();
            }

            if (backgroundJob.Count == 0 && timer != null) {
                timer.Stop();
                timer.Dispose();                
                timer = null;
            }
        }

    }
}
