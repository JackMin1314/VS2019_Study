using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoConf
{
    public class Video_Info
    {
        private string myid = "";
        private string mystrUrl = "";
        private string mystrUser = "";
        private string mystrPasswd = "";
        private string myopen = "";

        public string id { get { return myid; }set { myid = value; } }
        public string strUrl { get { return mystrUrl; }set { mystrUrl = value; } }
        public string strUser { get { return mystrUser; }set { mystrUser = value; } }
        public string strPasswd { get { return mystrPasswd; }set { mystrPasswd = value; } }
        public string isOpen { get { return myopen; }set { myopen = value; } }

    }
}
/*
 {
    "strNameComment": "name for this stream",
    "strName": "Stream 203",
    "strTokenComment": "token for this stream. must unique. if same. only first will be available",
    "strToken": "token5",
    "nTypeComment": "source type H5_FILE/H5_STREAM/H5_ONVIF",
    "nType": "H5_STREAM",
    "strUrlComment": "url(RTSP/RTMP...) or file path",
    "strUrl": "rtsp://admin:adm12345@192.168.10.203:554/Streaming/Channels/101",
    "strUserComment": "username",
    "strUser": "admin",
    "strPasswdComment": "password",
    "strPasswd": "adm12345",
    "bPasswdEncryptComment": "Password Encrypted",
    "bPasswdEncrypt": false,
    "bEnableAudioComment": "Enable Audio",
    "bEnableAudio": false,
    "nConnectTypeComment": "H5_ONDEMAND/H5_ALWAYS/H5_AUTO",
    "nConnectType": "H5_AUTO",
    "nRTSPTypeComment": "RTSP Connect protocol H5_RTSP_TCP/H5_RTSP_UDP/H5_RTSP_HTTP/H5_RTSP_HTTPS/H5_RTSP_AUTO",
    "nRTSPType": "H5_RTSP_AUTO",
    "strSrcIpAddressComment": "Ip Address for the device",
    "strSrcIpAddress": "192.168.0.1",
    "strSrcPortComment": "Port for the device",
    "strSrcPort": "80",
    "nChannelNumberComment": "Channel number (1-512)",
    "nChannelNumber": 1,
    "bOnvifProfileAutoComment": "ONVIF Auto select the video profile",
    "bOnvifProfileAuto": true,
    "strOnvifAddrComment": "ONVIF address (/onvif/device_service)",
    "strOnvifAddr": "/onvif/device_service",
    "strOnvifProfileMainComment": "ONVIF Main stream profile name",
    "strOnvifProfileMain": "Profile_1",
    "strOnvifProfileSubComment": "ONVIF Sub stream profile name",
    "strOnvifProfileSub": "Profile_2",
    "bRTSPPlaybackComment": "RTSP playback source",
    "bRTSPPlayback": false,
    "nRTSPPlaybackSpeedComment": "RTSP playback speed",
    "nRTSPPlaybackSpeed": 1
   },
 */
