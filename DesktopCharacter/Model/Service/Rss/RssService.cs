using System;
using System.ServiceModel.Syndication;
using System.Xml;

namespace DesktopCharacter.Model.Service.Rss
{
    //RSS渡せば喋ります(現在RSS1.0非対応)
    class RssService
    {
        public void ReturnRss(String url)
        {
            using (XmlReader reader = XmlReader.Create(url))
            {
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                foreach (SyndicationItem items in feed.Items)
                {
                    DesktopCharacter.Model.CharacterNotify.Instance.Talk("タイトル:" + items.Title.Text);
                }
            }
        }
    }
}