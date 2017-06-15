
//using System.Web.Script.Serialization;
using MediaBrowser.Controller.Devices;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Net;
using MediaBrowser.Controller.Session;
using MediaBrowser.Controller.Security;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.ApiClient;
using MediaBrowser.Common.Net;

//using MediaBrowser.Plugins.Yatse.Configuration;
using MediaBrowser.Model.Serialization;
using MediaBrowser.Model.Session;
using MediaBrowser.Model.Services;
using MediaBrowser.Plugins.FrontView;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
//using ServiceStack;

namespace MediaBrowser.Plugins.FrontView.Api
{
    [Route("/FrontView", "GET", Summary = "Returns NowPlaying information for Selected Device in Settings")]
 //   [Api(Description = "Returns NowPlaying information for Selected Device in Settings")]
    public class GetWeather : IReturn<FrontView.Api.YatseServerApi.ApiInfo>
    //public class GetWeather : IReturn<List<MediaBrowser.Model.Session.SessionInfoDto>>
    {
        [ApiMember(Name = "Location", Description = "Us zip / City, State, Country / City, Country", IsRequired = true, DataType = "string", ParameterType = "query", Verb = "GET")]
        public string Location { get; set; }
    }
    
    [Route("/FrontView/Play/{Command}", "POST", Summary="Issues Play State to controller already Identified Player")]
    //    [Api(Description = "Issues Play State to controller identified Player")]
    public class SendPlaystateCommand : IReturnVoid
    {
        /// <summary>
        /// Gets or sets the position to seek to
        /// </summary>
        [ApiMember(Name = "Id", Description = "Session Id", IsRequired = true, DataType = "string", ParameterType = "path", Verb = "POST")]
        public string Id { get; set; }

  
        [ApiMember(Name = "SeekPositionTicks", Description = "The position to seek to.", IsRequired = false, DataType = "string", ParameterType = "query", Verb = "POST")]
        public long? SeekPositionTicks { get; set; }

        [ApiMember(Name = "Command", Description = "The command to send - stop, pause, unpause, nexttrack, previoustrack, seek, fullscreen.", IsRequired = true, DataType = "string", ParameterType = "path", Verb = "POST")]
        public PlaystateCommand Command { get; set; }
    }
    [Route("/FrontView/Command/{Command}", "POST", Summary = "Issues a system command to a client")]
    [Authenticated]
    public class SendGeneralCommand : IReturnVoid
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [ApiMember(Name = "Id", Description = "Session Id", IsRequired = true, DataType = "string", ParameterType = "path", Verb = "POST")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The play command.</value>
        [ApiMember(Name = "Command", Description = "The command to send.", IsRequired = true, DataType = "string", ParameterType = "path", Verb = "POST")]
        public string Command { get; set; }
    }

    public static class Strings
    {
        
        //static Dictionary<string, string> foreign_characters = new Dictionary<string, string>
        static Dictionary<string, string> foreign_characters = (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Emby-Server\plugins\configurations\FrontViewForeignCharacters.txt")) ? new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Emby-Server\plugins\configurations\FrontViewForeignCharacters.txt")) : new Dictionary<string, string>
    {
        { "äæǽ", "ae" },
        { "öœ", "oe" },
        { "ü", "ue" },
        { "Ä", "Ae" },
        { "Ü", "Ue" },
        { "Ö", "Oe" },
        { "ÀÁÂÃÄÅǺĀĂĄǍΑΆẢẠẦẪẨẬẰẮẴẲẶА", "A" },
        { "àáâãåǻāăąǎªαάảạầấẫẩậằắẵẳặа", "a" },
        { "Б", "B" },
        { "б", "b" },
        { "ÇĆĈĊČ", "C" },
        { "çćĉċč", "c" },
        { "Д", "D" },
        { "д", "d" },
        { "ÐĎĐΔ", "Dj" },
        { "ðďđδ", "dj" },
        { "ÈÉÊËĒĔĖĘĚΕΈẼẺẸỀẾỄỂỆЕЭ", "E" },
        { "èéêëēĕėęěέεẽẻẹềếễểệеэ", "e" },
        { "Ф", "F" },
        { "ф", "f" },
        { "ĜĞĠĢΓГҐ", "G" },
        { "ĝğġģγгґ", "g" },
        { "ĤĦ", "H" },
        { "ĥħ", "h" },
        { "ÌÍÎÏĨĪĬǏĮİΗΉΊΙΪỈỊИЫ", "I" },
        { "ìíîïĩīĭǐįıηήίιϊỉịиыї", "i" },
        { "Ĵ", "J" },
        { "ĵ", "j" },
        { "ĶΚК", "K" },
        { "ķκк", "k" },
        { "ĹĻĽĿŁΛЛ", "L" },
        { "ĺļľŀłλл", "l" },
        { "М", "M" },
        { "м", "m" },
        { "ÑŃŅŇΝН", "N" },
        { "ñńņňŉνн", "n" },
        { "ÒÓÔÕŌŎǑŐƠØǾΟΌΩΏỎỌỒỐỖỔỘỜỚỠỞỢО", "O" },
        { "òóôõōŏǒőơøǿºοόωώỏọồốỗổộờớỡởợо", "o" },
        { "П", "P" },
        { "п", "p" },
        { "ŔŖŘΡР", "R" },
        { "ŕŗřρр", "r" },
        { "ŚŜŞȘŠΣС", "S" },
        { "śŝşșšſσςс", "s" },
        { "ȚŢŤŦτТ", "T" },
        { "țţťŧт", "t" },
        { "ÙÚÛŨŪŬŮŰŲƯǓǕǗǙǛŨỦỤỪỨỮỬỰУ", "U" },
        { "ùúûũūŭůűųưǔǖǘǚǜυύϋủụừứữửựу", "u" },
        { "ÝŸŶΥΎΫỲỸỶỴЙ", "Y" },
        { "ýÿŷỳỹỷỵй", "y" },
        { "В", "V" },
        { "в", "v" },
        { "Ŵ", "W" },
        { "ŵ", "w" },
        { "ŹŻŽΖЗ", "Z" },
        { "źżžζз", "z" },
        { "ÆǼ", "AE" },
        { "ß", "ss" },
        { "Ĳ", "IJ" },
        { "ĳ", "ij" },
        { "Œ", "OE" },
        { "ƒ", "f" },
        { "ξ", "ks" },
        { "π", "p" },
        { "β", "v" },
        { "μ", "m" },
        { "ψ", "ps" },
        { "Ё", "Yo" },
        { "ё", "yo" },
        { "Є", "Ye" },
        { "є", "ye" },
        { "Ї", "Yi" },
        { "Ж", "Zh" },
        { "ж", "zh" },
        { "Х", "Kh" },
        { "х", "kh" },
        { "Ц", "Ts" },
        { "ц", "ts" },
        { "Ч", "Ch" },
        { "ч", "ch" },
        { "Ш", "Sh" },
        { "ш", "sh" },
        { "Щ", "Shch" },
        { "щ", "shch" },
        { "ЪъЬь", "" },
        { "Ю", "Yu" },
        { "ю", "yu" },
        { "Я", "Ya" },
        { "я", "ya" },
        { "¿", " " },
        { "¡", " " }
    };
        
        


        public static char RemoveDiacritics(this char c)
        {
            foreach (KeyValuePair<string, string> entry in foreign_characters)
            {
                if (entry.Key.IndexOf(c) != -1)
                {
                    return entry.Value[0];
                }
            }
            return c;
        }

        public static string RemoveDiacritics(this string s)
        {
            //StringBuilder sb = new StringBuilder ();
            string text = "";
            
            //string Filenametouse = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\Emby-Server\plugins\configurations\FrontViewForeignCharacters.txt";
            //File.WriteAllText(Filenametouse, new JavaScriptSerializer().Serialize(foreign_characters));

            foreach (char c in s)
            {
                int len = text.Length;

                foreach (KeyValuePair<string, string> entry in foreign_characters)
                {
                    if (entry.Key.IndexOf(c) != -1)
                    {
                        text += entry.Value;
                        break;
                    }
                }

                if (len == text.Length)
                {
                    text += c;
                }
            }
            return text;
        }
    }
    
    




    public class YatseServerApi : IService
    {
      //  private readonly IJsonSerializer _json;
        private readonly ISessionManager _sessionManager;
        private readonly IUserManager _userManager;
        private readonly ILogger _logger;
        private readonly ILibraryManager _libraryManager;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IDeviceManager _deviceManager;
        

        public class ApiInfo
        {

            public string PlayingClientID { get; set; }
            public string ID {get;set;}
            public string Filename {get;set;}
            public bool IsPaused { get; set; }
            public string Overview { get; set; }
            public long? TimePosition { get; set; }
            public string Album { get; set; }
            public string Director { get; set; }
            public string Rating { get; set; }
            public string Tagline { get; set; }
            public string Studio { get; set; }
            public bool IsPlaying { get; set; }
            public string MediaType { get; set; }
            public string Title { get; set; }
            public int? Year { get; set; }
            public string Track { get; set; }
            public string Genre { get; set; }
            public string PrimaryItemId { get; set; }
            public string BackdropItemId { get; set; }
            public string Artist { get; set; }
            public int? EpisodeNumber { get; set; }
            public int? SeasonNumber { get; set; }
            public string ShowTitle { get; set; }
            public long? Duration { get; set; }
            public bool IsMuted { get; set; }
            public int? Volume { get; set; }
            //new to class 26.1.16
            public DateTime? AirDate { get; set; }
            public string NowViewingName { get; set; }
            public string NowViewingSeriesName { get; set; }
            public string NowViewingArtists { get; set; }

            public string NowViewingAlbum { get; set; }
            public string NowViewingMediaType { get; set; }

            public string AudioCodec { get; set; }
            public string AudioProfile { get; set; }
            public string AudioChannels { get; set; }
            public string VideoCodec { get; set; }
            public string VideoHeight { get; set; }
        }


        public YatseServerApi(ISessionManager sessionManager, ILogger logger, ILibraryManager libraryManager, IUserManager userManager, IJsonSerializer jsonSerializer, IDeviceManager deviceManager)
        {
            _logger = logger;
            _libraryManager = libraryManager;
            _sessionManager = sessionManager;
            _userManager = userManager;
            _jsonSerializer = jsonSerializer;
            _deviceManager = deviceManager;
            
        }

        public void Post(SendPlaystateCommand request)
        {
            var config = Plugin.Instance.Configuration;

            //Check Client Supports MediaControl

            // if (CheckSupportsMediaControl()==false)
            //   {
            //       _logger.Debug("FrontView+ -- Play Control -- Client does not support Media Control");
            //      return;
            //  }

            var ClientSessionID = GetClientID();


            var SessionID = config.SelectedDeviceId;



            _logger.Debug("--- FrontView+ Remote Command: Command Details: " + request.Command.ToString());


            var command = new PlaystateRequest
            {
                Command = request.Command,
                SeekPositionTicks = 0
                //   ControllingUserId = UserID        
            };

            _logger.Debug("--- FrontView+ PlayStateCommand: Command Sent: " + request.Command + " Session Id: " + SessionID + " Client Requesting ID: " + ClientSessionID);
            var task = _sessionManager.SendPlaystateCommand(SessionID, ClientSessionID, command, CancellationToken.None);
            Task.WaitAll(task);

        }

        public void Post(SendGeneralCommand request)
        {
            var config = Plugin.Instance.Configuration;
            var ClientSessionID = GetClientID();
            var SessionID = config.SelectedDeviceId;
            var UserID = GetUserIDNew();

            var command = new GeneralCommand
            {
                    Name = request.Command,
                    ControllingUserId = UserID.HasValue ? UserID.Value.ToString("N") : null
            };
            _logger.Debug("--- FrontView+ General Command: Command Sent: " + request.Command + " Session Id: " + SessionID + " Client Requesting ID: " + ClientSessionID);
            var task2 = _sessionManager.SendGeneralCommand(SessionID, ClientSessionID, command, CancellationToken.None);

            Task.WaitAll(task2);
         


        }


        //Get Session ID by checking Users - probably will need to update this and other to using DeviceID once selected.
        //No longer used - as DeviceID selection in Plugin Config takes over
        //Left here for perhaps future usage?
        public string GetSessionID()
        {
            var config = Plugin.Instance.Configuration;
            foreach (var session in _sessionManager.Sessions)
            {
                if (session.UserName == config.FrontViewUserName && session.DeviceId == config.FrontViewConfigID)
                {
                    _logger.Debug("FrontView+ -- GETSessionID -- Run -- Returning Session ID" + session.Id + ": DeviceName Name:" + session.DeviceName);
                    return session.Id;

                }
            }
            return "";
        }
        public string GetUserID()
        {
            var config = Plugin.Instance.Configuration;
            foreach (var session in _sessionManager.Sessions)
            {
                if (session.UserName == config.FrontViewUserName && session.DeviceId != config.FrontViewConfigID)
                {
                    _logger.Debug("FrontView+ -- GetUserID -- Run -- Returning Session ID" + session.Id + ": DeviceName Name:" + session.DeviceName);
                    var userid = session.UserId.Value.ToString("N");
                    return userid;       
                }
            }
            return null;
        }


        public string GetClientID()
        {
            var config = Plugin.Instance.Configuration;
            
            foreach (var session in _sessionManager.Sessions)
            {

              //  _logger.Debug("FrontView+ --- GetClientID ---- Checking.. Session.Id:" + session.Id + " UserId: " + session.UserId + "::::: to Match:" + config.SelectedDeviceId);
                if (session.Id == config.SelectedDeviceId)
                {
                    _logger.Debug("FrontView+ --- GetClientID -- Run -- Returning Session ID" + session.Id + ": DeviceName Name:" + session.DeviceName);
                    _logger.Debug("FrontView+ --- GetClientID ---- Checking SupportsMediaControl: " + session.SupportsMediaControl );
                 //   _logger.Debug("FrontView+ --- GetClientID ---- Checking session.UserID: " + session.UserId);
                    return session.Id;

                }
            }
            return "";
        }
        public Guid? GetUserIDNew()
        {
            var config = Plugin.Instance.Configuration;

            foreach (var session in _sessionManager.Sessions)
            {

                //  _logger.Debug("FrontView+ --- GetClientID ---- Checking.. Session.Id:" + session.Id + " UserId: " + session.UserId + "::::: to Match:" + config.SelectedDeviceId);
                if (session.Id == config.SelectedDeviceId)
                {
                    _logger.Debug("FrontView+ --- GetClientID -- Run -- Returning Session ID" + session.Id + ": UserID:" + session.UserId + ":UserName:"+session.UserName);
                    _logger.Debug("FrontView+ --- GetClientID ---- Checking SupportsMediaControl: " + session.SupportsMediaControl);
                    //   _logger.Debug("FrontView+ --- GetClientID ---- Checking session.UserID: " + session.UserId);
                    return session.UserId;

                }
            }
            return null;
        }
        public bool CheckSupportsMediaControl()
        {
            var config = Plugin.Instance.Configuration;
            foreach (var session in _sessionManager.Sessions)
            {
                if (session.Id == config.SelectedDeviceId)
                {
                    _logger.Debug("----- FrontView+ --- Check Supports Media Control  ---- Checking SupportsMediaControl: " + session.SupportsMediaControl);
                    if (session.SupportsMediaControl == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public object Get(GetWeather request)
        {
            string result = GetWeatherInfo();

            return result;
        }

/*
        
        static async Task YatsePlayCommand<T>(ISessionManager sessionManager, CancellationToken cancellation, MediaBrowser.Model.Session.PlayRequest Command)
        {
            var config = Plugin.Instance.Configuration;

            foreach (var session in sessionManager.Sessions)
            {
                if (session.UserName == config.YatseUserName && session.DeviceId != config.YatseConfigID)
                {
                    
                    await sessionManager.SendPlayCommand(session.Id, session.Id, Command, cancellation);
                }
            }
            //return true;
         }
         

        public async Task GetInfo()
        {
            var config = Plugin.Instance.Configuration;
            var InfotoSend = new ApiInfo();
            var cancellation = new CancellationToken();
            var Command = new MediaBrowser.Model.Session.PlayRequest();
            
            Command.PlayCommand = 0;
            //I believe this is Play Command.
            
            InfotoSend.Filename = "";
                       
            InfotoSend.ID = "Testing";
            InfotoSend.NowPlayingIsPaused = false;

            var NewData = "";

            _logger.Debug("--Yatse -- : YatsePlayCommand ");

            await YatsePlayCommand<Task>(_sessionManager, cancellation, Command).ConfigureAwait(false);
      
              


          //  NewData = _jsonSerializer.SerializeToString(InfotoSend);

          //  return NewData;

            //await _sessionManager.SendPlayCommand()
        }

        
        public string RemoveDiacritics(string text)
        {
            try
            {
                var normalizedString = text.Normalize(NormalizationForm.FormD);
                var stringBuilder = new StringBuilder();

                foreach (var c in normalizedString)
                {
                    var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                    if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    {
                        stringBuilder.Append(c);
                    }
                }

                return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            }
            catch
            {
                return "";
            }
        }
        */


        public string CheckDiacritics(string text)
        {
            var config = Plugin.Instance.Configuration;



            if (config.EnableDebugLogging == true)
            {
                return text.RemoveDiacritics();
            }
            else
            {
                return text;
            }
         
       }
 

        public string GetWeatherInfo()
        {

            var InfotoSend = new ApiInfo();
            var config = Plugin.Instance.Configuration;
            
            
            InfotoSend.PlayingClientID = config.SelectedDeviceId;
            InfotoSend.Filename = "";
            InfotoSend.ID = "";
            InfotoSend.IsPaused = false;
            InfotoSend.Overview = "";
            InfotoSend.Duration = 0;
            InfotoSend.TimePosition=0;    
            InfotoSend.Album ="";
            InfotoSend.Director="";
            InfotoSend.Rating ="";
            InfotoSend.Tagline ="";
            InfotoSend.Studio ="";
            InfotoSend.IsPlaying=false;
            InfotoSend.MediaType="";
            InfotoSend.Title =""; 
            InfotoSend.Year =0;   
            InfotoSend.Track="";   
            InfotoSend.Genre=""; 
            InfotoSend.PrimaryItemId = ""; 
            InfotoSend.BackdropItemId= ""; 
            InfotoSend.Artist="";           
            InfotoSend.EpisodeNumber=0;
            InfotoSend.SeasonNumber=0;
            InfotoSend.ShowTitle="";
            InfotoSend.Duration=0;
            InfotoSend.IsMuted= false;
            InfotoSend.Volume=0;
            InfotoSend.NowViewingName = "";
            InfotoSend.NowViewingSeriesName = "";
            InfotoSend.NowViewingArtists = "";
            InfotoSend.NowViewingAlbum = "";
            InfotoSend.NowViewingMediaType = "";
            InfotoSend.AudioChannels = "";
            InfotoSend.AudioCodec = "";
            InfotoSend.VideoCodec = "";
            InfotoSend.AudioProfile = "";
            InfotoSend.VideoHeight = "";

            var NewData = "";






            foreach (var session in _sessionManager.Sessions)
            {
                if (session.Id == config.SelectedDeviceId)
                {

                    // Should select Running Client with Username as configPage - defaults to Yatse
                    // then make sure it selects another client other than Yatse app instance.
                    // Does so by using ConfigID - which is set in Emby.Remote Yatse Codebase
                    if (session.PlayState != null)
                    {

                        InfotoSend.TimePosition = session.PlayState.PositionTicks;
                        InfotoSend.Volume = session.PlayState.VolumeLevel;
                        InfotoSend.IsMuted = session.PlayState.IsMuted;
                        InfotoSend.IsPaused = session.PlayState.IsPaused;



                    }
                    // delete below as well

                    if (session.NowPlayingItem != null)
                    {

                        InfotoSend.ID = string.IsNullOrEmpty(session.NowPlayingItem.Id) ? "" : session.NowPlayingItem.Id;




                        var AudioInfo = session.NowPlayingItem.MediaStreams.Where(item => item.Type == MediaBrowser.Model.Entities.MediaStreamType.Audio).FirstOrDefault();

                        var VideoInfo = session.NowPlayingItem.MediaStreams.Where(item => item.Type == MediaBrowser.Model.Entities.MediaStreamType.Video).FirstOrDefault();

                        if (AudioInfo != null)
                        {
                            InfotoSend.AudioChannels = string.IsNullOrEmpty(AudioInfo.Channels.ToString()) ? "" : AudioInfo.Channels.ToString();
                            InfotoSend.AudioCodec = string.IsNullOrEmpty(AudioInfo.Codec) ? "" : AudioInfo.Codec;
                            InfotoSend.AudioProfile = string.IsNullOrEmpty(AudioInfo.Profile) ? AudioInfo.Codec : AudioInfo.Profile;
                            //If AudioProfile info blank/doesn't exisit - send codec info instead.  Codec dca or ac3 etc. 
                        }


                        if (VideoInfo != null)
                        {
                            InfotoSend.VideoCodec = string.IsNullOrEmpty(VideoInfo.Codec) ? "" : VideoInfo.Codec;
                            InfotoSend.VideoHeight = string.IsNullOrEmpty(VideoInfo.Height.ToString()) ? "" : VideoInfo.Height.ToString();
                        }

                        //type =0 audio, 1 video







                        if (session.NowPlayingItem.Artists != null && !session.NowPlayingItem.Artists.Any())
                        {
                            foreach (var artist in session.NowPlayingItem.Artists)
                            {
                                if (artist != null)
                                {
                                    InfotoSend.Artist = CheckDiacritics(artist.ToString());
                                    
                                }
                            }
                        }

                        if (session.NowPlayingItem.PremiereDate.HasValue  && session.NowPlayingItem.PremiereDate != null)
                        {
                            InfotoSend.AirDate = session.NowPlayingItem.PremiereDate;
                        }
                        else
                        {
                            InfotoSend.AirDate = new DateTime(1900, 1, 1);
                        }






                        InfotoSend.MediaType = string.IsNullOrEmpty(session.NowPlayingItem.Type) ? "" : session.NowPlayingItem.Type;

                        InfotoSend.PrimaryItemId = string.IsNullOrEmpty(session.NowPlayingItem.ParentPrimaryImageItemId) ? "" : session.NowPlayingItem.ParentPrimaryImageItemId;

                        InfotoSend.EpisodeNumber = (session.NowPlayingItem.IndexNumber > 0) ? session.NowPlayingItem.IndexNumber : 0;
                        InfotoSend.SeasonNumber = (session.NowPlayingItem.ParentIndexNumber >0) ? session.NowPlayingItem.ParentIndexNumber : 0;
                        InfotoSend.Title = string.IsNullOrEmpty(session.NowPlayingItem.Name) ? "" : CheckDiacritics(session.NowPlayingItem.Name);
                        InfotoSend.ShowTitle = string.IsNullOrEmpty(session.NowPlayingItem.SeriesName) ? "" : CheckDiacritics(session.NowPlayingItem.SeriesName);
                        InfotoSend.BackdropItemId = string.IsNullOrEmpty(session.NowPlayingItem.ParentBackdropItemId) ? "" : session.NowPlayingItem.ParentBackdropItemId;
                        InfotoSend.Year = (session.NowPlayingItem.ProductionYear > 0) ? session.NowPlayingItem.ProductionYear : 1900;
                        InfotoSend.Album = string.IsNullOrEmpty(session.NowPlayingItem.Album) ? "" : CheckDiacritics(session.NowPlayingItem.Album);
                        

                        //Trying to get Director and/or other Information Artists etc.

                        var BaseItem = _libraryManager.GetItemById(session.NowPlayingItem.Id);

                        List<Controller.Entities.PersonInfo> ItemPeople = _libraryManager.GetPeople(BaseItem);
                        
                        bool index = ItemPeople.Any(item => item.Type == "Director");
                        if (index == true)
                        {
                            InfotoSend.Director = CheckDiacritics(ItemPeople.Find(i => i.Type == "Director").ToString());
                        }

                        var ItemData = _libraryManager.GetItemById(session.NowPlayingItem.Id);

                        if (ItemData != null)
                        {

                            InfotoSend.Overview = string.IsNullOrEmpty(ItemData.Overview) ? "" : CheckDiacritics(@ItemData.Overview);
                            foreach (var parent in ItemData.Parents)
                            {
                                InfotoSend.Tagline = string.IsNullOrEmpty(parent.Tags.FirstOrDefault()) ? "" : parent.Tags.FirstOrDefault();
                                
                                
                            }
                            
                            
                            foreach (var Genre in ItemData.Genres)
                            {
                                InfotoSend.Genre = string.IsNullOrEmpty(Genre.ToString()) ? "" : Genre.ToString();
                            }
                            InfotoSend.Rating = string.IsNullOrEmpty(ItemData.CommunityRating.ToString()) ? "" : ItemData.CommunityRating.ToString();
                            
                            InfotoSend.Duration = (ItemData.RunTimeTicks > 0) ? ItemData.RunTimeTicks : InfotoSend.TimePosition ;
                           
                            InfotoSend.Filename = string.IsNullOrEmpty(ItemData.Path) ? "" : ItemData.Path;
                            
                            if (!string.IsNullOrEmpty(InfotoSend.Filename))
                            {
                                    InfotoSend.IsPlaying = true;
                            }
                            else
                            {
                                InfotoSend.IsPlaying = false;
                            }

                            InfotoSend.Year = (ItemData.ProductionYear > 0) ? ItemData.ProductionYear : 1900;
                            foreach (var studio in ItemData.Studios)
                            {
                                InfotoSend.Studio = string.IsNullOrEmpty(studio.ToString()) ? "" : CheckDiacritics(studio.ToString());
                            }
                            
                        }


                    }
                    if (session.NowViewingItem != null)
                    {
                        InfotoSend.NowViewingName = string.IsNullOrEmpty(session.NowViewingItem.Name) ? "" : CheckDiacritics(session.NowViewingItem.Name);
                        InfotoSend.NowViewingSeriesName = string.IsNullOrEmpty(session.NowViewingItem.SeriesName) ? "" : CheckDiacritics(session.NowViewingItem.SeriesName);
                        InfotoSend.NowViewingArtists = string.IsNullOrEmpty(session.NowViewingItem.Artists.FirstOrDefault()) ? "" : CheckDiacritics(session.NowPlayingItem.Artists.FirstOrDefault());
                        InfotoSend.NowViewingAlbum = string.IsNullOrEmpty(session.NowViewingItem.Album) ? "" : session.NowViewingItem.Album;
                        InfotoSend.NowViewingMediaType = string.IsNullOrEmpty(session.NowViewingItem.MediaType) ? "" : session.NowViewingItem.MediaType;
                     

                    }
    
                }

            }

            
             //Testing code delete below soon
            
            //InfotoSend.ID = Plugin.Instance.Configuration.DevicesRunning

             
            
            NewData = _jsonSerializer.SerializeToString(InfotoSend);
                   
            return NewData;

        }

    }
}








