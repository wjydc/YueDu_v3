using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Config
{
    public class RecSection : IRecSection
    {
        public IBookIndex BookIndex { get; set; }

        public IBookDetail BookDetail { get; set; }

        public IBookChapterList BookChapterList { get; set; }

        public IBookChapterDetail BookChapterDetail { get; set; }

        public ILimitFree LimitFree { get; set; }

        public IRank Rank { get; set; }

        public ICartoonIndex CartoonIndex { get; set; }

        public ICartoonDetail CartoonDetail { get; set; }

        public ICartoonChapterList CartoonChapterList { get; set; }

        public RecSection()
        {
            BookIndex = new BookIndex();
            BookDetail = new BookDetail();
            BookChapterList = new BookChapterList();
            BookChapterDetail = new BookChapterDetail();
            LimitFree = new LimitFree();
            Rank = new Rank();
            CartoonIndex = new CartoonIndex();
            CartoonDetail = new CartoonDetail();
            CartoonChapterList = new CartoonChapterList();
        }
    }

    public class BookIndex : IBookIndex
    {
        public int MaleHotRec { get; set; }

        public int MaleNewRec { get; set; }

        public int MaleEditorRec { get; set; }

        public int MaleSaleRec { get; set; }

        public int FemaleHotRec { get; set; }

        public int FemaleNewRec { get; set; }

        public int FemaleEditorRec { get; set; }

        public int FemaleSaleRec { get; set; }

        public int MaleTopAd { get; set; }

        public int MaleMiddleAd { get; set; }

        public int FemaleTopAd { get; set; }

        public int FemaleMiddleAd { get; set; }

        public int ListenRec { get; set; }
    }

    public class BookDetail : IBookDetail
    {
        public int GuessRec { get; set; }
    }

    public class BookChapterList : IBookChapterList
    {
        public int GuessRec { get; set; }
    }

    public class BookChapterDetail : IBookChapterDetail
    {
        public int Rec { get; set; }
        public int Ad { get; set; }
    }

    public class LimitFree : ILimitFree
    {
        public int MaleIndexRec { get; set; }

        public int MaleNewRec { get; set; }

        public int MaleHotRec { get; set; }

        public int FemaleIndexRec { get; set; }

        public int FemaleNewRec { get; set; }

        public int FemaleHotRec { get; set; }

        public int MaleAd { get; set; }

        public int FemaleAd { get; set; }
    }

    public class Rank : IRank
    {
        public int MaleRec { get; set; }

        public int FemaleRec { get; set; }
    }

    public class CartoonIndex : ICartoonIndex
    {
        public int FavRec { get; set; }

        public int PopRec { get; set; }

        public int ThreeDRec { get; set; }

        public int TopAd { get; set; }

        public int MiddleAd { get; set; }
    }

    public class CartoonDetail : ICartoonDetail
    {
        public int GuessRec { get; set; }
    }

    public class CartoonChapterList : ICartoonChapterList
    {
        public int GuessRec { get; set; }
    }
}