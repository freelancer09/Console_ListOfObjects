using System;
using System.Collections.Generic;
using System.Text;

namespace Console_ListOfObjects
{
    public class VideoGame
    {
        #region ENUMS

        public enum Console
        {
            // first is default if not set
            none,
            Playstation,
            Xbox,
            Nintendo          
        }

        #endregion

        #region FIELDS

        private int _id;
        private string _name;
        private int _rating;
        private bool _isReleased;
        private Console _system;

        #endregion

        #region PROPERTIES

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Rating
        {
            get { return _rating; }
            set { _rating = value; }
        }
        public bool IsReleased
        {
            get { return _isReleased; }
            set { _isReleased = value; }
        }
        public Console System
        {
            get { return _system; }
            set { _system = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public VideoGame()
        {
            // Default constructor
        }
        public VideoGame(int id, string name, int rating, bool isReleased, Console system)
        {
            _id = id;
            _name = name;
            _rating = rating;
            _isReleased = isReleased;
            _system = system;
        }

        #endregion

        #region METHODS



        #endregion
    }
}
