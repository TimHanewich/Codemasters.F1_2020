using System;
using System.Collections.Generic;
using System.Drawing;

namespace Codemasters.F1_2020
{
    public static class CodemastersToolkit
        {
            public static PacketType GetPacketType(byte[] bytes)
            {
                if (bytes.Length == 1464)
                {
                    return PacketType.Motion;
                }
                else if (bytes.Length == 251)
                {
                    return PacketType.Session;
                }
                else if (bytes.Length == 1190)
                {
                    return PacketType.Lap;
                }
                else if (bytes.Length == 35)
                {
                    return PacketType.Event;
                }
                else if (bytes.Length == 1213)
                {
                    return PacketType.Participants;
                }
                else if (bytes.Length == 1102)
                {
                    return PacketType.CarSetup;
                }
                else if (bytes.Length == 1307)
                {
                    return PacketType.CarTelemetry;
                }
                else if (bytes.Length == 1344)
                {
                    return PacketType.CarStatus;
                }
                else if (bytes.Length == 839)
                {
                    return PacketType.FinalClassification;
                }
                else if (bytes.Length == 1169)
                {
                    return PacketType.LobbyInfo;
                }
                else
                {
                    throw new Exception("Packet type not recognized.");
                }
            }

            public static Track GetTrackFromTrackId(byte id)
            {
                Track ReturnTrack = Track.Unknown;

                if (id == 0)
                {
                    ReturnTrack = Track.Melbourne;
                }
                else if (id == 1)
                {
                    ReturnTrack = Track.PaulRicard;
                }
                else if (id == 2)
                {
                    ReturnTrack = Track.Shanghai;
                }
                else if (id == 3)
                {
                    ReturnTrack = Track.Sakhir;
                }
                else if (id == 4)
                {
                    ReturnTrack = Track.Catalunya;
                }
                else if (id == 5)
                {
                    ReturnTrack = Track.Monaco;
                }
                else if (id == 6)
                {
                    ReturnTrack = Track.Montreal;
                }
                else if (id == 7)
                {
                    ReturnTrack = Track.Silverstone;
                }
                else if (id == 8)
                {
                    ReturnTrack = Track.Hockenheim;
                }
                else if (id == 9)
                {
                    ReturnTrack = Track.Hungaroring;
                }
                else if (id == 10)
                {
                    ReturnTrack = Track.Spa;
                }
                else if (id == 11)
                {
                    ReturnTrack = Track.Monza;
                }
                else if (id == 12)
                {
                    ReturnTrack = Track.Singapore;
                }
                else if (id == 13)
                {
                    ReturnTrack = Track.Suzuka;
                }
                else if (id == 14)
                {
                    ReturnTrack = Track.AbuDhabi;
                }
                else if (id == 15)
                {
                    ReturnTrack = Track.Texas;
                }
                else if (id == 16)
                {
                    ReturnTrack = Track.Brazil;
                }
                else if (id == 17)
                {
                    ReturnTrack = Track.Austria;
                }
                else if (id == 18)
                {
                    ReturnTrack = Track.Sochi;
                }
                else if (id == 19)
                {
                    ReturnTrack = Track.Mexico;
                }
                else if (id == 20)
                {
                    ReturnTrack = Track.Baku;
                }
                else if (id == 21)
                {
                    ReturnTrack = Track.SakhirShort;
                }
                else if (id == 22)
                {
                    ReturnTrack = Track.SilverstoneShort;
                }
                else if (id == 23)
                {
                    ReturnTrack = Track.TexasShort;
                }
                else if (id == 24)
                {
                    ReturnTrack = Track.SuzukaShort;
                }
                else if (id == 25)
                {
                    ReturnTrack = Track.Hanoi;
                }
                else if (id == 26)
                {
                    ReturnTrack = Track.Zandvoort;
                }

                return ReturnTrack;
            }

            public static Driver GetDriverFromDriverId(byte id, Game game_)
            {
                Dictionary<byte, Driver> Dict = new Dictionary<byte, Driver>();

                if (game_ == Game.F1_2019)
                {
                    Dict.Add(0, Driver.CarlosSainz);
                    Dict.Add(1, Driver.DaniilKvyat);
                    Dict.Add(2, Driver.DanielRicciardo);
                    Dict.Add(6, Driver.KimiRaikkonen);
                    Dict.Add(7, Driver.LewisHamilton);
                    Dict.Add(9, Driver.MaxVerstappen);
                    Dict.Add(10, Driver.NicoHulkenburg);
                    Dict.Add(11, Driver.KevinMagnussen);
                    Dict.Add(12, Driver.RomainGrosjean);
                    Dict.Add(13, Driver.SebastianVettel);
                    Dict.Add(14, Driver.SergioPerez);
                    Dict.Add(15, Driver.ValtteriBottas);
                    Dict.Add(17, Driver.EstebanOcon);
                    Dict.Add(19, Driver.LanceStroll);
                    Dict.Add(20, Driver.ArronBarnes);
                    Dict.Add(21, Driver.MartinGiles);
                    Dict.Add(22, Driver.AlexMurray);
                    Dict.Add(23, Driver.LucasRoth);
                    Dict.Add(24, Driver.IgorCorreia);
                    Dict.Add(25, Driver.SophieLevasseur);
                    Dict.Add(26, Driver.JonasSchiffer);
                    Dict.Add(27, Driver.AlainForest);
                    Dict.Add(28, Driver.JayLetourneau);
                    Dict.Add(29, Driver.EstoSaari);
                    Dict.Add(30, Driver.YasarAtiyeh);
                    Dict.Add(31, Driver.CallistoCalabresi);
                    Dict.Add(32, Driver.NaotaIzum);
                    Dict.Add(33, Driver.HowardClarke);
                    Dict.Add(34, Driver.WilheimKaufmann);
                    Dict.Add(35, Driver.MarieLaursen);
                    Dict.Add(36, Driver.FlavioNieves);
                    Dict.Add(37, Driver.PeterBelousov);
                    Dict.Add(38, Driver.KlimekMichalski);
                    Dict.Add(39, Driver.SantiagoMoreno);
                    Dict.Add(40, Driver.BenjaminCoppens);
                    Dict.Add(41, Driver.NoahVisser);
                    Dict.Add(42, Driver.GertWaldmuller);
                    Dict.Add(43, Driver.JulianQuesada);
                    Dict.Add(44, Driver.DanielJones);
                    Dict.Add(45, Driver.ArtemMarkelov);
                    Dict.Add(46, Driver.TadasukeMakino);
                    Dict.Add(47, Driver.SeanGelael);
                    Dict.Add(48, Driver.NyckDeVries);
                    Dict.Add(49, Driver.JackAitken);
                    Dict.Add(50, Driver.GeorgeRussell);
                    Dict.Add(51, Driver.MaximilianGünther);
                    Dict.Add(52, Driver.NireiFukuzumi);
                    Dict.Add(53, Driver.LucaGhiotto);
                    Dict.Add(54, Driver.LandoNorris);
                    Dict.Add(55, Driver.SergioSetteCamara);
                    Dict.Add(56, Driver.LouisDeletraz);
                    Dict.Add(57, Driver.AntonioFuoco);
                    Dict.Add(58, Driver.CharlesLeclerc);
                    Dict.Add(59, Driver.PierreGasly);
                    Dict.Add(62, Driver.AlexanderAlbon);
                    Dict.Add(63, Driver.NicholasLatifi);
                    Dict.Add(64, Driver.DorianBoccolacci);
                    Dict.Add(65, Driver.NikoKari);
                    Dict.Add(66, Driver.RobertoMerhi);
                    Dict.Add(67, Driver.ArjunMaini);
                    Dict.Add(68, Driver.AlessioLorandi);
                    Dict.Add(69, Driver.RubenMeijer);
                    Dict.Add(70, Driver.RashidNair);
                    Dict.Add(71, Driver.JackTremblay);
                    Dict.Add(74, Driver.AntonioGiovinazzi);
                    Dict.Add(75, Driver.RobertKubica);
                    Dict.Add(78, Driver.NobuharuMatsushita);
                    Dict.Add(79, Driver.NikitaMazepin);
                    Dict.Add(80, Driver.GuanyaZhou);
                    Dict.Add(81, Driver.MickSchumacher);
                    Dict.Add(82, Driver.CallumIlott);
                    Dict.Add(83, Driver.JuanManuelCorrea);
                    Dict.Add(84, Driver.JordanKing);
                    Dict.Add(85, Driver.MahaveerRaghunathan);
                    Dict.Add(86, Driver.TatianaCalderon);
                    Dict.Add(87, Driver.AnthoineHubert);
                    Dict.Add(88, Driver.GuilianoAlesi);
                    Dict.Add(89, Driver.RalphBoschung);

                }

                Driver d = Driver.Unknown;
                foreach (KeyValuePair<byte, Driver> kvp in Dict)
                {
                    if (kvp.Key == id)
                    {
                        d = kvp.Value;
                    }
                }

                return d;

            }

            public static Team GetTeamFromTeamId(byte id)
            {
                Dictionary<byte, Team> Dict = new Dictionary<byte, Team>();

                Dict.Add(0, Team.Mercedes);
                Dict.Add(1, Team.Ferrari);
                Dict.Add(2, Team.RedBullRacing);
                Dict.Add(3, Team.Williams);
                Dict.Add(4, Team.RacingPoint);
                Dict.Add(5, Team.Renault);
                Dict.Add(6, Team.AlphaTauri);
                Dict.Add(7, Team.Haas);
                Dict.Add(8, Team.McLaren);
                Dict.Add(9, Team.AlfaRomeo);
                Dict.Add(10, Team.McLaren1988);
                Dict.Add(11, Team.McLaren1991);
                Dict.Add(12, Team.Williams1992);
                Dict.Add(13, Team.Ferrari1995);
                Dict.Add(14, Team.Williams1996);
                Dict.Add(15, Team.McLaren1998);
                Dict.Add(16, Team.Ferrari2002);
                Dict.Add(17, Team.Ferrari2004);
                Dict.Add(18, Team.Renault2006);
                Dict.Add(19, Team.Ferrari2007);
                Dict.Add(20, Team.McLaren2008);
                Dict.Add(21, Team.RedBull2010);
                Dict.Add(22, Team.Ferrari1976);
                Dict.Add(23, Team.ARTGrandPrix);
                Dict.Add(24, Team.CamposVexatecRacing);
                Dict.Add(25, Team.Carlin);
                Dict.Add(26, Team.CharouzRacingSystem);
                Dict.Add(27, Team.DAMS);
                Dict.Add(28, Team.RussianTime);
                Dict.Add(29, Team.MPMotorsport);
                Dict.Add(30, Team.Pertamina);
                Dict.Add(31, Team.McLaren1990);
                Dict.Add(32, Team.Trident);
                Dict.Add(33, Team.BWTArden);
                Dict.Add(34, Team.McLaren1976);
                Dict.Add(35, Team.Lotus1972);
                Dict.Add(36, Team.Ferrari1979);
                Dict.Add(37, Team.McLaren1982);
                Dict.Add(38, Team.Williams2003);
                Dict.Add(39, Team.Brawn2009);
                Dict.Add(40, Team.Lotus1978);
                Dict.Add(41, Team.F1GenericCar);
                Dict.Add(42, Team.ArtGP19);
                Dict.Add(43, Team.Campos19);
                Dict.Add(44, Team.Carlin19);
                Dict.Add(45, Team.SauberJuniorCharouz19);
                Dict.Add(46, Team.Dams19);
                Dict.Add(47, Team.UniVirtuosi19);
                Dict.Add(48, Team.MPMotorsport19);
                Dict.Add(49, Team.Prema19);
                Dict.Add(50, Team.Trident19);
                Dict.Add(51, Team.Arden19);
                Dict.Add(53, Team.Benetton1994);
                Dict.Add(55, Team.Ferrari2000);
                Dict.Add(56, Team.Jordan1991);
                Dict.Add(255, Team.MyTeam);

                Team tr = Team.Unknown;
                foreach (KeyValuePair<byte, Team> kvp in Dict)
                {
                    if (kvp.Key == id)
                    {
                        tr = kvp.Value;
                    }
                }

                return tr;
            }

            public static SurfaceType GetSurfaceTypeFromSurfaceTypeId(byte id)
            {
                if (id == 0)
                {
                    return SurfaceType.Tarmac;
                }
                else if (id == 1)
                {
                    return SurfaceType.RumbleStrip;
                }
                else if (id == 2)
                {
                    return SurfaceType.Concrete;
                }
                else if (id == 3)
                {
                    return SurfaceType.Rock;
                }
                else if (id == 4)
                {
                    return SurfaceType.Gravel;
                }
                else if (id == 5)
                {
                    return SurfaceType.Mud;
                }
                else if (id == 6)
                {
                    return SurfaceType.Sand;
                }
                else if (id == 7)
                {
                    return SurfaceType.Grass;
                }
                else if (id == 8)
                {
                    return SurfaceType.Water;
                }
                else if (id == 9)
                {
                    return SurfaceType.Cobblestone;
                }
                else if (id == 10)
                {
                    return SurfaceType.Metal;
                }
                else if (id == 11)
                {
                    return SurfaceType.Ridged;
                }
                else
                {
                    return SurfaceType.Unknown;
                }
            }

            public static Color GetTeamColorByTeam(Team t)
            {


                Color c = Color.FromArgb(255, 255, 255, 255);

                if (t == Team.Mercedes)
                {
                    c = Color.FromArgb(255, 0, 210, 190);
                }
                else if (t == Team.Haas)
                {
                    c = Color.FromArgb(255, 120, 120, 120);
                }
                else if (t == Team.McLaren)
                {
                    c = Color.FromArgb(255, 255, 135, 0);
                }
                else if (t == Team.AlfaRomeo)
                {
                    c = Color.FromArgb(255, 155, 0, 0);
                }
                else if (t == Team.RedBullRacing)
                {
                    c = Color.FromArgb(255, 6, 0, 239);
                }
                else if (t == Team.Renault)
                {
                    c = Color.FromArgb(255, 255, 245, 0);
                }
                else if (t == Team.Ferrari)
                {
                    c = Color.FromArgb(255, 220, 0, 0);
                }
                else if (t == Team.AlphaTauri)
                {
                    c = Color.FromArgb(255, 255, 255, 255);
                }
                else if (t == Team.Williams)
                {
                    c = Color.FromArgb(255, 0, 130, 250);
                }
                else if (t == Team.RacingPoint)
                {
                    c = Color.FromArgb(255, 245, 150, 200);
                }

                return c;
            }

            /// <summary>
            /// This returns the name that should be displayed in a leaderboard (i.e. "L. Hamilton")
            /// </summary>
            public static string GetDriverDisplayNameFromDriver(Driver d)
            {
                string s = "Unknown";

                if (d == Driver.LewisHamilton)
                {
                    s = "L. Hamilton";
                }
                else if (d == Driver.ValtteriBottas)
                {
                    s = "V. Bottas";
                }
                else if (d == Driver.RomainGrosjean)
                {
                    s = "R. Grosjean";
                }
                else if (d == Driver.KevinMagnussen)
                {
                    s = "K. Magnussen";
                }
                else if (d == Driver.ValtteriBottas)
                {
                    s = "V. Bottas";
                }
                else if (d == Driver.CarlosSainz)
                {
                    s = "C. Sainz";
                }
                else if (d == Driver.LandoNorris)
                {
                    s = "L. Norris";
                }
                else if (d == Driver.KimiRaikkonen)
                {
                    s = "K. Raikkonen";
                }
                else if (d == Driver.AntonioGiovinazzi)
                {
                    s = "A. Giovinazzi";
                }
                else if (d == Driver.MaxVerstappen)
                {
                    s = "M. Verstappen";
                }
                else if (d == Driver.AlexanderAlbon)
                {
                    s = "A. Albon";
                }
                else if (d == Driver.DanielRicciardo)
                {
                    s = "D. Ricciardo";
                }
                else if (d == Driver.NicoHulkenburg)
                {
                    s = "N. Hulkenburg";
                }
                else if (d == Driver.SebastianVettel)
                {
                    s = "S. Vettel";
                }
                else if (d == Driver.CharlesLeclerc)
                {
                    s = "C. Leclerc";
                }
                else if (d == Driver.PierreGasly)
                {
                    s = "P. Gasly";
                }
                else if (d == Driver.DaniilKvyat)
                {
                    s = "D. Kvyat";
                }
                else if (d == Driver.GeorgeRussell)
                {
                    s = "G. Russell";
                }
                else if (d == Driver.NicholasLatifi)
                {
                    s = "N. Latifi";
                }
                else if (d == Driver.RobertKubica)
                {
                    s = "R. Kubica";
                }
                else if (d == Driver.SergioPerez)
                {
                    s = "S. Perez";
                }
                else if (d == Driver.LanceStroll)
                {
                    s = "L. Stroll";
                }
                else if (d == Driver.EstebanOcon)
                {
                    s = "E. Ocon";
                }
                else if (d == Driver.NicholasLatifi)
                {
                    s = "N. Latifi";
                }

                return s;
            }

            public static string GetLapTimeDisplayFromSeconds(float seconds)
            {
                int number_of_minutes = (int)Math.Floor(seconds / 60);
                float remaining = seconds - (number_of_minutes * 60);
                string s = number_of_minutes.ToString() + ":" + remaining.ToString("#00.000");
                return s;
            }            
        
            public static string CarNameFromTeam(Team t)
            {
                string ToReturn = "2020 Challenger";

                if (t == Team.Mercedes)
                {
                    ToReturn = "W11";
                }
                else if (t == Team.RedBullRacing)
                {
                    ToReturn = "RB16";
                }
                else if (t == Team.McLaren)
                {
                    ToReturn = "MCL35";
                }
                else if (t == Team.Ferrari)
                {
                    ToReturn = "SF1000";
                }
                else if (t == Team.RacingPoint)
                {
                    ToReturn = "RP20";
                }
                else if (t == Team.Renault)
                {
                    ToReturn = "R.S.20";
                }
                else if (t == Team.AlphaTauri)
                {
                    ToReturn = "AT01";
                }
                else if (t == Team.AlfaRomeo)
                {
                    ToReturn = "C39";
                }
                else if (t == Team.Haas)
                {
                    ToReturn = "VF-20";
                }
                else if (t == Team.Williams)
                {
                    ToReturn = "FW43";
                }

                return ToReturn;
            }

            #region "Friendly Names"

            public static string GetTrackFriendlyName(Track t)
            {
                string ToReturn = t.ToString();

                if (t == Track.Melbourne)
                {
                    ToReturn = "Melbourne";
                }
                else if (t == Track.PaulRicard)
                {
                    ToReturn = "Paul Ricard";
                }
                else if (t == Track.Shanghai)
                {
                    ToReturn = "China";
                }
                else if (t == Track.Sakhir)
                {
                    ToReturn = "Bahrain";
                }
                else if (t == Track.Catalunya)
                {
                    ToReturn = "Spain";
                }
                else if (t == Track.Monaco)
                {
                    ToReturn = "Monaco";
                }
                else if (t == Track.Montreal)
                {
                    ToReturn = "Canada";
                }
                else if (t == Track.Silverstone)
                {
                    ToReturn = "Silverstone";
                }
                else if (t == Track.Hockenheim)
                {
                    ToReturn = "Hockenheim";
                }
                else if (t == Track.Hungaroring)
                {
                    ToReturn = "Hungaroring";
                }
                else if (t == Track.Spa)
                {
                    ToReturn = "Spa (Belgium)";
                }
                else if (t == Track.Monza)
                {
                    ToReturn = "Italy";
                }
                else if (t == Track.Singapore)
                {
                    ToReturn = "Singapore";
                }
                else if (t == Track.Suzuka)
                {
                    ToReturn = "Japan";
                }
                else if (t == Track.AbuDhabi)
                {
                    ToReturn = "Abu Dhabi";
                }
                else if (t == Track.Texas)
                {
                    ToReturn = "United States";
                }
                else if (t == Track.Brazil)
                {
                    ToReturn = "Brazil";
                }
                else if (t == Track.Austria)
                {
                    ToReturn = "Austria";
                }
                else if (t == Track.Sochi)
                {
                    ToReturn = "Russia";
                }
                else if (t == Track.Mexico)
                {
                    ToReturn = "Mexico";
                }
                else if (t == Track.Baku)
                {
                    ToReturn = "Azerbaijan";
                }
                else if (t == Track.SakhirShort)
                {
                    ToReturn = "Bahrain (Short)";
                }
                else if (t == Track.SilverstoneShort)
                {
                    ToReturn = "Silverstone (Short)";
                }
                else if (t == Track.TexasShort)
                {
                    ToReturn = "US (Short)";
                }
                else if (t == Track.SuzukaShort)
                {
                    ToReturn = "Japan (Short)";
                }
                else if (t == Track.Hanoi)
                {
                    ToReturn = "Vietnam";
                }
                else if (t == Track.Zandvoort)
                {
                    ToReturn = "Netherlands";
                }

                return ToReturn;
            }

            public static string GetDriverFriendlyName(Driver d)
            {
                string ToReturn = d.ToString();

                if (d == Driver.LewisHamilton)
                {
                    ToReturn = "Lewis Hamilton";
                }
                else if (d == Driver.ValtteriBottas)
                {
                    ToReturn = "Valtteri Bottas";
                }
                else if (d == Driver.CharlesLeclerc)
                {
                    ToReturn = "Charles Leclerc";
                }
                else if (d == Driver.SebastianVettel)
                {
                    ToReturn = "Sebastian Vettel";
                }
                else if (d == Driver.MaxVerstappen)
                {
                    ToReturn = "Max Verstappen";
                }
                else if (d == Driver.AlexanderAlbon)
                {
                    ToReturn = "Alex Albon";
                }
                else if (d == Driver.CarlosSainz)
                {
                    ToReturn = "Carlos Sainz";
                }
                else if (d == Driver.LandoNorris)
                {
                    ToReturn = "Lando Norris";
                }
                else if (d == Driver.DanielRicciardo)
                {
                    ToReturn = "Daniel Ricciardo";
                }
                else if (d == Driver.EstebanOcon)
                {
                    ToReturn = "Esteban Ocon";
                }
                else if (d == Driver.PierreGasly)
                {
                    ToReturn = "Pierre Gasly";
                }
                else if (d == Driver.DaniilKvyat)
                {
                    ToReturn = "Daniil Kvyat";
                }
                else if (d == Driver.SergioPerez)
                {
                    ToReturn = "Sergio Perez";
                }
                else if (d == Driver.LanceStroll)
                {
                    ToReturn = "Lance Stroll";
                }
                else if (d == Driver.KimiRaikkonen)
                {
                    ToReturn = "Kimi Raikkonen";
                }
                else if (d == Driver.AntonioGiovinazzi)
                {
                    ToReturn = "Antonio Giovinazzi";
                }
                else if (d == Driver.KevinMagnussen)
                {
                    ToReturn = "Kevin Magnussen";
                }
                else if (d == Driver.RomainGrosjean)
                {
                    ToReturn = "Romain Grosjean";
                }
                else if (d == Driver.GeorgeRussell)
                {
                    ToReturn = "George Russell";
                }
                else if (d == Driver.NicholasLatifi)
                {
                    ToReturn = "Nicholas Latifi";
                }

                return ToReturn;
            }

            public static string GetSessionTypeFriendlyName(SessionPacket.SessionType session_type)
            {
                string ToReturn = session_type.ToString();

                if (session_type == SessionPacket.SessionType.Practice)
                {
                    ToReturn = "Practice";
                }
                else if (session_type == SessionPacket.SessionType.Qualifying)
                {
                    ToReturn = "Qualifying";
                }
                else if (session_type == SessionPacket.SessionType.Race)
                {
                    ToReturn = "Race";
                }
                else if (session_type == SessionPacket.SessionType.Practice1)
                {
                    ToReturn = "Practice 1";
                }
                else if (session_type == SessionPacket.SessionType.Practice2)
                {
                    ToReturn = "Practice 2";
                }
                else if (session_type == SessionPacket.SessionType.Practice3)
                {
                    ToReturn = "Practice 3";
                }
                else if (session_type == SessionPacket.SessionType.ShortPractice)
                {
                    ToReturn = "Short Practice";
                }
                else if (session_type == SessionPacket.SessionType.Qualifying1)
                {
                    ToReturn = "Qualifying 1";
                }
                else if (session_type == SessionPacket.SessionType.Qualifying2)
                {
                    ToReturn = "Qualifying 2";
                }
                else if (session_type == SessionPacket.SessionType.Qualifying3)
                {
                    ToReturn = "Qualifying 3";
                }
                else if (session_type == SessionPacket.SessionType.ShortQualifying)
                {
                    ToReturn = "Short Qualifying";
                }
                else if (session_type == SessionPacket.SessionType.OneShotQualifying)
                {
                    ToReturn = "One-Shot Qualifying";
                }
                else if (session_type == SessionPacket.SessionType.Race2)
                {
                    ToReturn = "Race #2";
                }
                else if (session_type == SessionPacket.SessionType.TimeTrial)
                {
                    ToReturn = "Time Trial";
                }

                return ToReturn;
            }

            public static string GetTeamFriendlyName(Team t)
            {
                string ToReturn = t.ToString();

                if (t == Team.Mercedes)
                {
                    ToReturn = "Mercedes";
                }
                else if (t == Team.Ferrari)
                {
                    ToReturn = "Ferrari";
                }
                else if (t == Team.RedBullRacing)
                {
                    ToReturn = "Red Bull";
                }
                else if (t == Team.McLaren)
                {
                    ToReturn = "McLaren";
                }
                else if (t == Team.Renault)
                {
                    ToReturn = "Renault";
                }
                else if (t == Team.AlphaTauri)
                {
                    ToReturn = "Alpha Tauri";
                }
                else if (t == Team.RacingPoint)
                {
                    ToReturn = "Racing Point";
                }
                else if (t == Team.AlfaRomeo)
                {
                    ToReturn = "Alfa Romeo";
                }
                else if (t == Team.Haas)
                {
                    ToReturn = "Haas";
                }
                else if (t == Team.Williams)
                {
                    ToReturn = "Williams";
                }

                return ToReturn;
            }

            public static string GetDriverStatusFriendlyName(LapPacket.DriverStatus driver_status)
            {
                string ToReturn = "?";

                if (driver_status == LapPacket.DriverStatus.FlyingLap)
                {
                    ToReturn = "Flying Lap";
                }
                else if (driver_status == LapPacket.DriverStatus.InGarage)
                {
                    ToReturn = "In Garage";
                }
                else if (driver_status == LapPacket.DriverStatus.InLap)
                {
                    ToReturn = "In Lap";
                }
                else if (driver_status == LapPacket.DriverStatus.OnTrack)
                {
                    ToReturn = "On Track";
                }
                else if (driver_status == LapPacket.DriverStatus.OutLap)
                {
                    ToReturn = "Out Lap";
                }
                
                return ToReturn;
            }

            #endregion
        
        }

}