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

        protected double _ratingA;
        protected double _ratingB;

        protected double _scoreA;
        protected double _scoreB;

        protected double _expectedA;
        protected double _expectedB;

        protected double _newRatingA;
        protected double _newRatingB;

        /**
         * Constructor function which does all the maths and stores the results ready
         * for retrieval.
         *
         * @param int ratingA Current rating of A
         * @param int ratingB Current rating of B
         * @param int scoreA Score of A
         * @param int scoreB Score of B
         */

        public Rating(double ratingA, double ratingB, double scoreA, double scoreB)
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

        public Rating setNewSettings(double ratingA, double ratingB, double scoreA, double scoreB)
        {
            _ratingA = ratingA;
            _ratingB = ratingB;
            _scoreA = scoreA;
            _scoreB = scoreB;

            List<double> expectedScores = _getExpectedScores(_ratingA, _ratingB);
            _expectedA = expectedScores[0];
            _expectedB = expectedScores[1];

            List<double> newRatingsList = _getNewRatings(_ratingA, _ratingB, _expectedA, _expectedB, _scoreA, _scoreB);
            _newRatingA = newRatingsList[0];
            _newRatingB = newRatingsList[1];

            return this;
        }

        /**
         * Retrieve the calculated data.
         *
         * @return List<double> A list containing the new ratings for A and B.
        */
        public List<double> getNewRatings()
        {
            List<double> newRatingsList = new List<double>
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
        protected List<double> _getExpectedScores(double ratingA, double ratingB)
        {
            double expectedScoreA = 1 / (1 + (Math.Pow(10, ( ratingB - ratingA) / 400)) );
            double expectedScoreB = 1 / (1 + (Math.Pow(10, ( ratingA - ratingB) / 400)) );

            List<double> expectedScoresList = new List<double>
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
        protected List<double> _getNewRatings(double ratingA, double ratingB, double expectedA, double expectedB, double scoreA, double scoreB)
        {
            double newRatingA = ratingA + (Rating.KFACTOR * ( scoreA - expectedA ) );
            double newRatingB = ratingB + (Rating.KFACTOR * ( scoreB - expectedB ) );

            List<double> newRatingList = new List<double>
            {
                newRatingA,
                newRatingB
            };

            return newRatingList;
        }
    }
}
