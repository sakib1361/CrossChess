﻿using CrossChess.Helpers;
using CrossChess.Services;
using CrossChessEngine.Model.Data;
using CrossChessEngine.Model.PieceModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CrossChess.Converters
{
    public class PieceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Piece piece)
            {
               var col =  piece.Player.Colour;
                Color pieceCol = col == Player.PlayerColourNames.Black ? Color.Black : Color.White;
                FontAwesomeIconType icon = FontAwesomeIconType.Chess ;
                switch (piece.Name)
                {
                    case PieceNames.Bishop:
                        icon = FontAwesomeIconType.ChessBishop;
                        break;
                    case PieceNames.King:
                        icon = FontAwesomeIconType.ChessKing;
                        break;
                    case PieceNames.Knight:
                        icon = FontAwesomeIconType.ChessKnight;
                            break;
                    case PieceNames.Pawn:
                        icon = FontAwesomeIconType.ChessPawn;
                        break;
                    case PieceNames.Queen:
                        icon = FontAwesomeIconType.ChessQueen;
                        break;
                    case PieceNames.Rook:
                        icon = FontAwesomeIconType.ChessRook;
                        break;
                }

                return FontIconFont.GetSource(icon, pieceCol);
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}