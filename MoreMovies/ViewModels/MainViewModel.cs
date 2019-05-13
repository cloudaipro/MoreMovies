using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using MoreMovies.Models;
using NHibernate;
using System.Reactive.Disposables;

namespace MoreMovies.ViewModels
{
    public class MainViewModel
    {
        [Reactive]
        public virtual ReactiveCommand<MetroWindow, Task> CmdDataMaintenance { get; set; }
        public virtual ObservableCollection<Title_Akas> Title_Akases { get; set; } = new ObservableCollection<Title_Akas>();
        public virtual ObservableCollection<Name_Basics> Name_Basics { get; set; } = new ObservableCollection<Name_Basics>();
        public int BatchSize = 1000;
        public MainViewModel()
        {
            CmdDataMaintenance = ReactiveCommand.Create(async (MetroWindow w) => {
                ITransaction xact = DBBO.StatelessSession.BeginTransaction();//Session.BeginTransaction();
                //IStatelessSession session = DBBO.StatelessSession
                int dataCnt = 0;
                
                await CreateDataToDB(w, "Dataset\\name.basics.tsv.gz", "Dataset\\name.basics.tsv", 
                    (fields) => {
                        DBBO.StatelessSession.Insert(new Name_Basics
                        {
                            //nconst	primaryName	birthYear	deathYear	primaryProfession	knownForTitles
                            nconst = fields[0],
                            primaryName = ToStr(fields[1]),
                            birthYear = ToInt(fields[2]),
                            deathYear = ToInt(fields[3]),
                            primaryProfession = ToStr(fields[4]),
                            knownForTitles = ToStr(fields[5])
                        });
                        if (dataCnt++ % BatchSize == 0)
                        {
                            try { xact.Commit(); }
                            catch (Exception ex)
                            {
                                xact.Rollback();
                                using (StreamWriter outputFile = new StreamWriter("data.log", true))
                                {
                                    outputFile.WriteLine(ex.Message);
                                }
                            }
                            xact = DBBO.StatelessSession.BeginTransaction();
                        }
                    });
                try { xact.Commit(); }
                catch (Exception ex)
                {
                    xact.Rollback();
                    using (StreamWriter outputFile = new StreamWriter("data.log", true))
                    {
                        outputFile.WriteLine(ex.Message);
                    }
                }

                dataCnt = 0;
                xact = DBBO.StatelessSession.BeginTransaction();
                await CreateDataToDB(w, "Dataset\\title.akas.tsv.gz", "Dataset\\title.akas.tsv",
                    (fields) => {
                        DBBO.StatelessSession.Insert(new Title_Akas
                        {
                            //titleId	ordering	title	region	language	types	attributes	isOriginalTitle
                            titleId = fields[0],
                            ordering = ToInt(fields[1]),
                            title = ToStr(fields[2]),
                            region = ToStr(fields[3]),
                            language = ToStr(fields[4]),
                            types = ToStr(fields[5]),
                            attributes = ToStr(fields[6]),
                            isOriginalTitle = ToBool(fields[7])
                        });
                        if (dataCnt++ % BatchSize == 0)
                        {
                            try { xact.Commit(); }
                            catch (Exception ex)
                            {
                                xact.Rollback();
                                using (StreamWriter outputFile = new StreamWriter("data.log", true))
                                {
                                    outputFile.WriteLine(ex.Message);
                                }
                            }
                            xact = DBBO.StatelessSession.BeginTransaction();
                        }
                    });
                try { xact.Commit(); }
                catch (Exception ex)
                {
                    xact.Rollback();
                    using (StreamWriter outputFile = new StreamWriter("data.log", true))
                    {
                        outputFile.WriteLine(ex.Message);
                    }
                }

                dataCnt = 0;
                xact = DBBO.StatelessSession.BeginTransaction();
                await CreateDataToDB(w, "Dataset\\title.basics.tsv.gz", "Dataset\\title.basics.tsv",
                    (fields) => {
                        DBBO.StatelessSession.Insert(new Title_Basics
                        {
                            //tconst	titleType	primaryTitle	originalTitle	isAdult	startYear	endYear	runtimeMinutes	genres
                            tconst = fields[0],
                            titleType = ToStr(fields[1]),
                            primaryTitle = ToStr(fields[2]),
                            originalTitle = ToStr(fields[3]),
                            isAdult = ToBool(fields[4]),
                            startYear = ToInt(fields[5]),
                            endYear = ToInt(fields[6]),
                            runtimeMinutes = ToInt(fields[7]),
                            genres = ToStr(fields[8])
                        });
                        if (dataCnt++ % BatchSize == 0)
                        {
                            try { xact.Commit(); }
                            catch (Exception ex)
                            {
                                xact.Rollback();
                                using (StreamWriter outputFile = new StreamWriter("data.log", true))
                                {
                                    outputFile.WriteLine(ex.Message);
                                }
                            }
                            xact = DBBO.StatelessSession.BeginTransaction();
                        }
                    });
                try { xact.Commit(); }
                catch (Exception ex)
                {
                    xact.Rollback();
                    using (StreamWriter outputFile = new StreamWriter("data.log", true))
                    {
                        outputFile.WriteLine(ex.Message);
                    }
                }

                dataCnt = 0;
                xact = DBBO.StatelessSession.BeginTransaction();
                await CreateDataToDB(w, "Dataset\\title.crew.tsv.gz", "Dataset\\title.crew.tsv",
                    (fields) => {
                        DBBO.StatelessSession.Insert(new Title_Crew
                        {
                            //tconst	directors	writers
                            tconst = fields[0],
                            directors = ToStr(fields[1]),
                            writers = ToStr(fields[2])
                        });
                        if (dataCnt++ % BatchSize == 0)
                        {
                            try { xact.Commit(); }
                            catch (Exception ex)
                            {
                                xact.Rollback();
                                using (StreamWriter outputFile = new StreamWriter("data.log", true))
                                {
                                    outputFile.WriteLine(ex.Message);
                                }
                            }
                            xact = DBBO.StatelessSession.BeginTransaction();
                        }
                    });
                try { xact.Commit(); }
                catch (Exception ex)
                {
                    xact.Rollback();
                    using (StreamWriter outputFile = new StreamWriter("data.log", true))
                    {
                        outputFile.WriteLine(ex.Message);
                    }
                }

                dataCnt = 0;
                xact = DBBO.StatelessSession.BeginTransaction();
                await CreateDataToDB(w, "Dataset\\title.episode.tsv.gz", "Dataset\\title.episode.tsv",
                    (fields) => {
                        DBBO.StatelessSession.Insert(new Title_Episode
                        {
                            //tconst	titleType	primaryTitle	originalTitle	isAdult	startYear	endYear	runtimeMinutes	genres
                            tconst = fields[0],
                            parentTconst = ToStr(fields[1]),
                            seasonNumber = ToInt(fields[2]),
                            episodeNumber = ToInt(fields[3])
                        });
                        if (dataCnt++ % BatchSize == 0)
                        {
                            try { xact.Commit(); }
                            catch (Exception ex)
                            {
                                xact.Rollback();
                                using (StreamWriter outputFile = new StreamWriter("data.log", true))
                                {
                                    outputFile.WriteLine(ex.Message);
                                }
                            }
                            xact = DBBO.StatelessSession.BeginTransaction();
                        }
                    });
                try { xact.Commit(); }
                catch (Exception ex)
                {
                    xact.Rollback();
                    using (StreamWriter outputFile = new StreamWriter("data.log", true))
                    {
                        outputFile.WriteLine(ex.Message);
                    }
                }

                dataCnt = 0;
                xact = DBBO.StatelessSession.BeginTransaction();
                await CreateDataToDB(w, "Dataset\\title.principals.tsv.gz", "Dataset\\title.principals.tsv",
                    (fields) => {
                        DBBO.StatelessSession.Insert(new Title_Principals
                        {
                            //tconst	titleType	primaryTitle	originalTitle	isAdult	startYear	endYear	runtimeMinutes	genres
                            tconst = fields[0],
                            ordering = ToInt(fields[1]),
                            nconst = ToStr(fields[2]),
                            category = ToStr(fields[3]),
                            job = ToStr(fields[4]),
                            characters = ToStr(fields[5])
                        });
                        if (dataCnt++ % BatchSize == 0)
                        {
                            try { xact.Commit(); }
                            catch (Exception ex)
                            {
                                xact.Rollback();
                                using (StreamWriter outputFile = new StreamWriter("data.log", true))
                                {
                                    outputFile.WriteLine(ex.Message);
                                }
                            }
                            xact = DBBO.StatelessSession.BeginTransaction();
                        }
                    });
                try { xact.Commit(); }
                catch (Exception ex)
                {
                    xact.Rollback();
                    using (StreamWriter outputFile = new StreamWriter("data.log", true))
                    {
                        outputFile.WriteLine(ex.Message);
                    }
                }
                
                dataCnt = 0;
                xact = DBBO.StatelessSession.BeginTransaction();
                await CreateDataToDB(w, "Dataset\\title.ratings.tsv.gz", "Dataset\\title.ratings.tsv",
                    (fields) => {
                        DBBO.StatelessSession.Insert(new Title_Ratings
                        {
                            //tconst	titleType	primaryTitle	originalTitle	isAdult	startYear	endYear	runtimeMinutes	genres
                            tconst = fields[0],
                            averageRating = ToDouble(fields[1]),
                            numVotes = ToInt(fields[2])
                        });
                        if (dataCnt++ % BatchSize == 0)
                        {
                            try { xact.Commit(); }
                            catch (Exception ex)
                            {
                                xact.Rollback();
                                using (StreamWriter outputFile = new StreamWriter("data.log", true))
                                {
                                    outputFile.WriteLine(ex.Message);
                                }
                            }
                            xact = DBBO.StatelessSession.BeginTransaction();
                        }
                    });
                try { xact.Commit(); }
                catch (Exception ex)
                {
                    xact.Rollback();
                    using (StreamWriter outputFile = new StreamWriter("data.log", true))
                    {
                        outputFile.WriteLine(ex.Message);
                    }
                }


            });

        }

        private int? ToInt(string s)
        {
            try
            {
                return Int32.Parse(s);

            }
            catch (Exception)
            {
                return null;
            }               
        }
        private float? ToFloat(string s)
        {
            try
            {
                return  (float)Math.Round(float.Parse(s), 1);

            }
            catch (Exception)
            {
                return null;
            }
        }
        private double ToDouble(string s)
        {
            try
            {
                return Math.Round(Double.Parse(s), 1);

            }
            catch (Exception)
            {
                return 0;
            }
        }
        private bool ToBool(string s)
        {
            try
            {
                return (string.IsNullOrWhiteSpace(s)) ? false:
                                           (s == "1") ? true : false;

            }
            catch (Exception)
            {
                return false;
            }
        }

        private string ToStr(string s)
        {
            try
            {
                return (s == "\\N") ? "" : s;

            }
            catch (Exception)
            {
                return null;
            }

        }


        private async Task CreateDataToDB(MetroWindow w, string gzName, string tsvName, Action<string[]> createData)
        {
            var mySettings = new MetroDialogSettings()
            {
                NegativeButtonText = "Close now",
                AnimateShow = false,
                AnimateHide = false
            };

            var controller = await w.ShowProgressAsync("Please wait...", "Decompress "+ gzName + "!", settings: mySettings);
            //controller.SetIndeterminate();
            if (!File.Exists(tsvName))
                await Decompress(new FileInfo(gzName));
            controller.Minimum = 0;
            controller.Maximum = 100;
            controller.SetProgress(0);
            
            controller.SetCancelable(true);
            await Task.Run(() =>
            {
                var fileSize = new FileInfo(tsvName).Length;
                var stream = new System.IO.StreamReader(tsvName);
                int progress = stream.ReadLine().Length + 1;
                double dPercentage = 0;
                var allRecords = Observable.Create<string[]>(observer =>
                {
                    string line;
                    int idx = 0;
                    while ((line = stream.ReadLine()) != null)
                    {
                        observer.OnNext(line.Split('\t'));
                        try
                        {
                            progress += line.Length + 1;
                            if (++idx % BatchSize != 0) continue;
                            dPercentage = Math.Round(progress * 100.0 / fileSize, 2);
                            if (dPercentage > 100) dPercentage = 100;
                            controller.SetProgress(dPercentage);
                            controller.SetMessage("Create data " + tsvName + " ... " + idx + " - " + dPercentage + "%");
                            if (controller.IsCanceled)
                                break;
                        }
                        catch (Exception){}
                    }
                    if (controller.IsCanceled == false)
                        controller.SetProgress(100);
                    observer.OnCompleted();
                    return Disposable.Empty;
                });

                
                allRecords.Subscribe(createData);
            });

            await controller.CloseAsync();

            if (controller.IsCanceled)
            {
                await w.ShowMessageAsync("No cupcakes!", "You stopped baking!");
            }
            else
            {
                await w.ShowMessageAsync("Cupcakes!", "Your cupcakes are finished! Enjoy!");
            }
        }

        public Task Decompress(FileInfo fileToDecompress)
        {
            return Task.Run(() =>
            {
                using (FileStream originalFileStream = fileToDecompress.OpenRead())
                {
                    string currentFileName = fileToDecompress.FullName;
                    string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                    using (FileStream decompressedFileStream = File.Create(newFileName))
                    {
                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                            //Console.WriteLine($"Decompressed: {fileToDecompress.Name}");
                        }
                    }
                }
            });
        }
    }

}
