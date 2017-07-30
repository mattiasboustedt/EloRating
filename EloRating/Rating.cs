using System;
using System.Collections.Generic;

/**
 * This class calculates ratings based on the Elo system used in chess.
 *
 * @author Mattias Boustedt
 * @translated from https://github.com/Chovanec/elo-rating by Michael Chovanec
 * @license Creative Commons Attribution 4.0 International License
 */

namespace Rating
{
    public class Rating
    {
        private const int KFACTOR = 16;

        protected int _ratingA;
        protected int _ratingB;

        protected int _scoreA;
        protected int _scoreB;

        protected int _expectedA;
        protected int _expectedB;

        protected int _newRatingA;
        protected int _newRatingB;

        /**
         * Constructor function which does all the maths and stores the results ready
         * for retrieval.
         *
         * @param int ratingA Current rating of A
         * @param int ratingB Current rating of B
         * @param int scoreA Score of A
         * @param int scoreB Score of B
         */

        public Rating(int ratingA, int ratingB, int scoreA, int scoreB)
        {
            setNewSettings(ratingA, ratingB, scoreA, scoreB);
        }

        /**
        * Set new input data.
        *
        * @param int ratingA Current rating of A
        * @param int ratingB Current rating of B
        * @param int scoreA Score of A
        * @param int scoreB Score of B
        * @return self
        */

        public Rating setNewSettings(int ratingA, int ratingB, int scoreA, int scoreB)
        {
            _ratingA = ratingA;
            _ratingB = ratingB;
            _scoreA = scoreA;
            _scoreB = scoreB;

            List<int> expectedScores = _getExpectedScores(_ratingA, _ratingB);
            _expectedA = expectedScores[0];
            _expectedB = expectedScores[1];

            List<int> newRatingsList = _getNewRatings(_ratingA, _ratingB, _expectedA, _expectedB, _scoreA, _scoreB);
            _newRatingA = newRatingsList[0];
            _newRatingB = newRatingsList[1];

            return this;
        }

        /**
         * Retrieve the calculated data.
         *
         * @return List<int> A list containing the new ratings for A and B.
        */
        public List<int> getNewRatings()
        {
            List<int> newRatingsList = new List<int>
            {
                _newRatingA,
                _newRatingB
            };

            return newRatingsList;
        }

        // Protected & private functions begin here
        /**
         * @param int ratingA The Rating of Player A
         * @param int ratingB The Rating of Player B
         * @return List<int>
         */
        protected List<int> _getExpectedScores(int ratingA, int ratingB)
        {
            int expectedScoreA = 1 / (1 + (IntPow(10, ( ratingB - ratingA) / 400)) );
            int expectedScoreB = 1 / (1 + (IntPow(10, ( ratingA - ratingB) / 400)) );

            List<int> expectedScoresList = new List<int>
            {
                expectedScoreA,
                expectedScoreB
            };

            return expectedScoresList;
        }

        /**
         * @param int ratingA The Rating of Player A
         * @param int ratingB The Rating of Player A
         * @param int expectedA The expected score of Player A
         * @param int expectedB The expected score of Player B
         * @param int scoreA The score of Player A
         * @param int scoreB The score of Player B
         * @return List<int>
         */
        protected List<int> _getNewRatings(int ratingA, int ratingB, int expectedA, int expectedB, int scoreA, int scoreB)
        {
            int newRatingA = ratingA + (Rating.KFACTOR * ( scoreA - expectedA ) );
            int newRatingB = ratingB + (Rating.KFACTOR * ( scoreB - expectedB ) );

            List<int> newRatingList = new List<int>
            {
                newRatingA,
                newRatingB
            };

            return newRatingList;
        }

        public int IntPow(int x, int pow)
        {
            return (int)Math.Pow(x, pow);
        }
    }
}
