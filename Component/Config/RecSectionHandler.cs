using Model.Config;
using System.Xml;
using Utility;

namespace Component.Config
{
    public class RecSectionHandler : BaseSectionHandler
    {
        public override object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            IRecSection model = null;

            if (section != null)
            {
                model = new RecSection();

                XmlNode node = null;

                #region bookindex

                if (GetSingleNodeValue(section, "/RecSection/book/index/male", out node))
                {
                    model.BookIndex.MaleHotRec = GetSingleNodeAttributeValue(node, "rec/item[@key=\"hot\"]", "value").ToInt();
                    model.BookIndex.MaleNewRec = GetSingleNodeAttributeValue(node, "rec/item[@key=\"new\"]", "value").ToInt();
                    model.BookIndex.MaleEditorRec = GetSingleNodeAttributeValue(node, "rec/item[@key=\"editor\"]", "value").ToInt();
                    model.BookIndex.MaleSaleRec = GetSingleNodeAttributeValue(node, "rec/item[@key=\"sale\"]", "value").ToInt();
                    model.BookIndex.MaleTopAd = GetSingleNodeAttributeValue(node, "ad/item[@key=\"top\"]", "value").ToInt();
                    model.BookIndex.MaleMiddleAd = GetSingleNodeAttributeValue(node, "ad/item[@key=\"middle\"]", "value").ToInt();
                }
                if (GetSingleNodeValue(section, "/RecSection/book/index/female", out node))
                {
                    model.BookIndex.FemaleHotRec = GetSingleNodeAttributeValue(node, "rec/item[@key=\"hot\"]", "value").ToInt();
                    model.BookIndex.FemaleNewRec = GetSingleNodeAttributeValue(node, "rec/item[@key=\"new\"]", "value").ToInt();
                    model.BookIndex.FemaleEditorRec = GetSingleNodeAttributeValue(node, "rec/item[@key=\"editor\"]", "value").ToInt();
                    model.BookIndex.FemaleSaleRec = GetSingleNodeAttributeValue(node, "rec/item[@key=\"sale\"]", "value").ToInt();
                    model.BookIndex.FemaleTopAd = GetSingleNodeAttributeValue(node, "ad/item[@key=\"top\"]", "value").ToInt();
                    model.BookIndex.FemaleMiddleAd = GetSingleNodeAttributeValue(node, "ad/item[@key=\"middle\"]", "value").ToInt();
                }
                model.BookIndex.ListenRec = GetSingleNodeAttributeValue(section, "/RecSection/book/index/rec[@type=\"listen\"]/item[@key=\"rec\"]", "value").ToInt();

                #endregion bookindex

                #region bookdetail

                if (GetSingleNodeValue(section, "/RecSection/book/detail/rec", out node))
                {
                    model.BookDetail.GuessRec = GetSingleNodeAttributeValue(node, "item[@key=\"guess\"]", "value").ToInt();
                }

                #endregion bookdetail

                #region bookchapterlist

                if (GetSingleNodeValue(section, "/RecSection/book/chapterlist/rec", out node))
                {
                    model.BookChapterList.GuessRec = GetSingleNodeAttributeValue(node, "item[@key=\"guess\"]", "value").ToInt();
                }

                #endregion bookchapterlist

                #region bookchapterdetail

                if (GetSingleNodeValue(section, "/RecSection/book/chapter", out node))
                {
                    model.BookChapterDetail.Rec = GetSingleNodeAttributeValue(node, "rec/item[@key=\"rec\"]", "value").ToInt();
                    model.BookChapterDetail.Ad = GetSingleNodeAttributeValue(node, "ad/item[@key=\"ad\"]", "value").ToInt();
                }

                #endregion bookchapterdetail

                #region limitfree

                if (GetSingleNodeValue(section, "/RecSection/book/limitfree/male/rec", out node))
                {
                    model.LimitFree.MaleIndexRec = GetSingleNodeAttributeValue(node, "item[@key=\"index\"]", "value").ToInt();
                    model.LimitFree.MaleNewRec = GetSingleNodeAttributeValue(node, "item[@key=\"new\"]", "value").ToInt();
                    model.LimitFree.MaleHotRec = GetSingleNodeAttributeValue(node, "item[@key=\"hot\"]", "value").ToInt();
                }
                if (GetSingleNodeValue(section, "/RecSection/book/limitfree/female/rec", out node))
                {
                    model.LimitFree.FemaleIndexRec = GetSingleNodeAttributeValue(node, "item[@key=\"index\"]", "value").ToInt();
                    model.LimitFree.FemaleNewRec = GetSingleNodeAttributeValue(node, "item[@key=\"new\"]", "value").ToInt();
                    model.LimitFree.FemaleHotRec = GetSingleNodeAttributeValue(node, "item[@key=\"hot\"]", "value").ToInt();
                }

                #endregion limitfree

                #region rank

                if (GetSingleNodeValue(section, "/RecSection/book/rank/rec", out node))
                {
                    model.Rank.MaleRec = GetSingleNodeAttributeValue(node, "item[@key=\"male\"]", "value").ToInt();
                    model.Rank.FemaleRec = GetSingleNodeAttributeValue(node, "item[@key=\"female\"]", "value").ToInt();
                }

                #endregion rank

                #region cartoonindex

                if (GetSingleNodeValue(section, "/RecSection/cartoon/index/rec", out node))
                {
                    model.CartoonIndex.FavRec = GetSingleNodeAttributeValue(node, "item[@key=\"fav\"]", "value").ToInt();
                    model.CartoonIndex.PopRec = GetSingleNodeAttributeValue(node, "item[@key=\"pop\"]", "value").ToInt();
                    model.CartoonIndex.ThreeDRec = GetSingleNodeAttributeValue(node, "item[@key=\"three\"]", "value").ToInt();
                }
                if (GetSingleNodeValue(section, "/RecSection/cartoon/index/ad", out node))
                {
                    model.CartoonIndex.TopAd = GetSingleNodeAttributeValue(node, "item[@key=\"top\"]", "value").ToInt();
                    model.CartoonIndex.MiddleAd = GetSingleNodeAttributeValue(node, "item[@key=\"middle\"]", "value").ToInt();
                }

                #endregion cartoonindex

                #region cartoondetail

                if (GetSingleNodeValue(section, "/RecSection/cartoon/detail/rec", out node))
                {
                    model.CartoonDetail.GuessRec = GetSingleNodeAttributeValue(node, "item[@key=\"guess\"]", "value").ToInt();
                }

                #endregion cartoondetail

                #region cartoonchapterlist

                if (GetSingleNodeValue(section, "/RecSection/cartoon/chapterlist/rec", out node))
                {
                    model.CartoonChapterList.GuessRec = GetSingleNodeAttributeValue(node, "item[@key=\"guess\"]", "value").ToInt();
                }

                #endregion cartoonchapterlist
            }

            return model;
        }
    }
}