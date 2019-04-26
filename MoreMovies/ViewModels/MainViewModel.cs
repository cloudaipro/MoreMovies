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

namespace MoreMovies.ViewModels
{
    public class MainViewModel
    {
        [Reactive]
        public virtual ReactiveCommand<MetroWindow, Task> CmdDataMaintenance { get; set; }

        public MainViewModel()
        {
            CmdDataMaintenance = ReactiveCommand.Create(async (MetroWindow w) => {
                await ShowProgressDialog(w);
            });

        }

        private async Task ShowProgressDialog(MetroWindow w)
        {
            var mySettings = new MetroDialogSettings()
            {
                NegativeButtonText = "Close now",
                AnimateShow = false,
                AnimateHide = false
            };

            var controller = await w.ShowProgressAsync("Please wait...", "Decompress title.akas.tsv.gz!", settings: mySettings);
            controller.SetIndeterminate();

            await Decompress(new FileInfo("title.akas.tsv.gz"));//Task.Delay(5000);

            controller.SetCancelable(true);
            await Task.Run(() => 
            {
                var lines = File.ReadAllLines(@"title.akas.tsv");
                int idx = 0;
                IObservable<string[]> allRecords = lines.Skip(1)
                                                        .Select(line => line.Split('\t'))
                                                        .ToObservable();
                allRecords.Subscribe((fields) =>
                          {
                              if (++idx % 100 == 0)
                              {
                                  controller.SetMessage("Baking cupcake(" + idx + "): " + fields[2] + " ...");
                              }
                          });
                controller.SetMessage("Total Baking cupcake: " + idx + ".");
            });


            double i = 0.0;
            while (i < 6.0)
            {
                double val = (i / 100.0) * 20.0;
                controller.SetProgress(val);
                controller.SetMessage("Baking cupcake: " + i + "...");

                if (controller.IsCanceled)
                    break; //canceled progressdialog auto closes.

                i += 1.0;

                await Task.Delay(2000);
            }

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
