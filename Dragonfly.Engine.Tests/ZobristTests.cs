﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Dragonfly.Engine.CoreTypes;
using Dragonfly.Engine.MoveGeneration;
using NUnit.Framework;

namespace Dragonfly.Engine.Tests
{
    [TestFixture]
    class ZobristTests
    {
        private MoveGen _moveGen;

        [SetUp]
        public void Setup()
        {
            _moveGen = new MoveGen();
        }

        [TestCaseSource(typeof(ZobristData), nameof(ZobristData.ZobristTestCases))]
        public void ZobristTest(string fen, int depth)
        {
            var board = BoardParsing.PositionFromFen(fen);
            ZobristTestHelper(board, depth);
        }

        private void ZobristTestHelper(Position position, int depth)
        {
            Assert.AreEqual(position.ZobristHash, ZobristHashing.CalculateFullHash(position));
            if (depth <= 0)
                return;

            var moves = new List<Move>();
            _moveGen.Generate(moves, position);

            foreach (var move in moves)
            {
                var updatedBoard = Position.MakeMove(new Position(), move, position);

                if (!_moveGen.OnlyLegalMoves && updatedBoard.MovedIntoCheck())
                    continue;
                
                ZobristTestHelper(updatedBoard, depth-1);
            }
        }
    }

    public class ZobristData
    {
        private static readonly Regex PerftResultRegex = new Regex(@"D(\d+) (\d+)");
        public static IEnumerable ZobristTestCases
        {
            get
            {
                foreach (var line in Enumerators.GetPerftCasesEnumerator())
                {
                    var splitLine = line.Split(';', 2);

                    var fen = splitLine[0].Trim();
                    var perftCases = splitLine[1];

                    foreach (var perftCase in perftCases.Split(';', StringSplitOptions.RemoveEmptyEntries))
                    {
                        var match = PerftResultRegex.Match(perftCase);
                        int depth = int.Parse(match.Groups[1].Value);

                        // we want to reduce all depths by 1; no need to do expensive tests
                        depth--;
                        if (depth < 1)
                            continue;

                        yield return new TestCaseData(fen, depth);
                    }
                }
            }
        }
    }
}
