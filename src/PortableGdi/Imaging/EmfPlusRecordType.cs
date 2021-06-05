﻿/*
 * EmfPlusRecordType.cs - Implementation of the
 *			"System.Drawing.Imaging.EmfPlusRecordType" class.
 *
 * Copyright (C) 2003  Southern Storm Software, Pty Ltd.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

namespace Portable.Drawing.Imaging
{

public enum EmfPlusRecordType
{
	None					= 0,
	EmfMin						= 1,
	EmfHeader					= 1,
	EmfPolyBezier				= 2,
	EmfPolygon					= 3,
	EmfPolyline					= 4,
	EmfPolyBezierTo				= 5,
	EmfPolyLineTo				= 6,
	EmfPolyPolyline				= 7,
	EmfPolyPolygon				= 8,
	EmfSetWindowExtEx			= 9,
	EmfSetWindowOrgEx			= 10,
	EmfSetViewportExtEx			= 11,
	EmfSetViewportOrgEx			= 12,
	EmfSetBrushOrgEx			= 13,
	EmfEof						= 14,
	EmfSetPixelV				= 15,
	EmfSetMapperFlags			= 16,
	EmfSetMapMode				= 17,
	EmfSetBkMode				= 18,
	EmfSetPolyFillMode			= 19,
	EmfSetROP2					= 20,
	EmfSetStretchBltMode		= 21,
	EmfSetTextAlign				= 22,
	EmfSetColorAdjustment		= 23,
	EmfSetTextColor				= 24,
	EmfSetBkColor				= 25,
	EmfOffsetClipRgn			= 26,
	EmfMoveToEx					= 27,
	EmfSetMetaRgn				= 28,
	EmfExcludeClipRect			= 29,
	EmfIntersectClipRect		= 30,
	EmfScaleViewportExtEx		= 31,
	EmfScaleWindowExtEx			= 32,
	EmfSaveDC					= 33,
	EmfRestoreDC				= 34,
	EmfSetWorldTransform		= 35,
	EmfModifyWorldTransform		= 36,
	EmfSelectObject				= 37,
	EmfCreatePen				= 38,
	EmfCreateBrushIndirect		= 39,
	EmfDeleteObject				= 40,
	EmfAngleArc					= 41,
	EmfEllipse					= 42,
	EmfRectangle				= 43,
	EmfRoundRect				= 44,
	EmfRoundArc					= 45,
	EmfChord					= 46,
	EmfPie						= 47,
	EmfSelectPalette			= 48,
	EmfCreatePalette			= 49,
	EmfSetPaletteEntries		= 50,
	EmfResizePalette			= 51,
	EmfRealizePalette			= 52,
	EmfExtFloodFill				= 53,
	EmfLineTo					= 54,
	EmfArcTo					= 55,
	EmfPolyDraw					= 56,
	EmfSetArcDirection			= 57,
	EmfSetMiterLimit			= 58,
	EmfBeginPath				= 59,
	EmfEndPath					= 60,
	EmfCloseFigure				= 61,
	EmfFillPath					= 62,
	EmfStrokeAndFillPath		= 63,
	EmfStrokePath				= 64,
	EmfFlattenPath				= 65,
	EmfWidenPath				= 66,
	EmfSelectClipPath			= 67,
	EmfAbortPath				= 68,
	EmfReserved069				= 69,
	EmfGdiComment				= 70,
	EmfFillRgn					= 71,
	EmfFrameRgn					= 72,
	EmfInvertRgn				= 73,
	EmfPaintRgn					= 74,
	EmfExtSelectClipRgn			= 75,
	EmfBitBlt					= 76,
	EmfStretchBlt				= 77,
	EmfMaskBlt					= 78,
	EmfPlgBlt					= 79,
	EmfSetDIBitsToDevice		= 80,
	EmfStretchDIBits			= 81,
	EmfExtCreateFontIndirect	= 82,
	EmfExtTextOutA				= 83,
	EmfExtTextOutW				= 84,
	EmfPolyBezier16				= 85,
	EmfPolygon16				= 86,
	EmfPolyline16				= 87,
	EmfPolyBezierTo16			= 88,
	EmfPolylineTo16				= 89,
	EmfPolyPolyline16			= 90,
	EmfPolyPolygon16			= 91,
	EmfPolyDraw16				= 92,
	EmfCreateMonoBrush			= 93,
	EmfCreateDibPatternBrushPt	= 94,
	EmfExtCreatePen				= 95,
	EmfPolyTextOutA				= 96,
	EmfPolyTextOutW				= 97,
	EmfSetIcmMode				= 98,
	EmfCreateColorSpace			= 99,
	EmfSetColorSpace			= 100,
	EmfDeleteColorSpace			= 101,
	EmfGlsRecord				= 102,
	EmfGlsBoundedRecord			= 103,
	EmfPixelFormat				= 104,
	EmfDrawEscape				= 105,
	EmfExtEscape				= 106,
	EmfStartDoc					= 107,
	EmfSmallTextOut				= 108,
	EmfForceUfiMapping			= 109,
	EmfNamedEscpae				= 110,
	EmfColorCorrectPalette		= 111,
	EmfSetIcmProfileA			= 112,
	EmfSetIcmProfileW			= 113,
	EmfAlphaBlend				= 114,
	EmfSetLayout				= 115,
	EmfTransparentBlt			= 116,
	EmfReserved117				= 117,
	EmfGradientFill				= 118,
	EmfSetLinkedUfis			= 119,
	EmfSetTextJustification		= 120,
	EmfColorMatchToTargetW		= 121,
	EmfCreateColorSpaceW		= 122,
	EmfPlusRecordBase			= 16384,
	Invalid						= 16384,
	Min							= 16385,
	Header						= 16385,
	EndOfFile					= 16386,
	Comment						= 16387,
	GetDC						= 16388,
	MultiFormatStart			= 16389,
	MultiFormatSection			= 16390,
	MultiFormatEnd				= 16391,
	Object						= 16392,
	Clear						= 16393,
	FillRects					= 16394,
	DrawRects					= 16395,
	FillPolygon					= 16396,
	DrawLines					= 16397,
	FillEllipse					= 16398,
	DrawEllipse					= 16399,
	FillPie						= 16400,
	DrawPie						= 16401,
	DrawArc						= 16402,
	FillRegion					= 16403,
	FillPath					= 16404,
	DrawPath					= 16405,
	FillClosedCurve				= 16406,
	DrawClosedCurve				= 16407,
	DrawCurve					= 16408,
	DrawBeziers					= 16409,
	DrawImage					= 16410,
	DrawImagePoints				= 16411,
	DrawString					= 16412,
	SetRenderingOrigin			= 16413,
	SetAntiAliasMode			= 16414,
	SetTextRenderingHint		= 16415,
	SetTextContrast				= 16416,
	SetInterpolationMode		= 16417,
	SetPixelOffsetMode			= 16418,
	SetCompositingMode			= 16419,
	SetCompositingQuality		= 16420,
	Save						= 16421,
	Restore						= 16422,
	BeginContainer				= 16423,
	BeginContainerNoParams		= 16424,
	EndContainer				= 16425,
	SetWorldTransform			= 16426,
	ResetWorldTransform			= 16427,
	MultiplyWorldTransform		= 16428,
	TranslateWorldTransform		= 16429,
	ScaleWorldTransform			= 16430,
	RotateWorldTransform		= 16431,
	SetPageTransform			= 16432,
	ResetClip					= 16433,
	SetClipRect					= 16434,
	SetClipPath					= 16435,
	SetClipRegion				= 16436,
	OffsetClip					= 16437,
	DrawDriverString			= 16438,
	Max							= 16438,
	Total						= 16439,
	WmfRecordBase				= 65536,
	WmfSaveDC					= 65566,
	WmfRealizePalette			= 65589,
	WmfSetPalEntries			= 65591,
	WmfCreatePalette			= 65783,
	WmfSetBkMode				= 65794,
	WmfSetMapMode				= 65795,
	WmfSetROP2					= 65796,
	WmfSetRelAbs				= 65797,
	WmfSetPolyFillMode			= 65798,
	WmfSetStretchBltMode		= 65799,
	WmfSetTextCharExtra			= 65800,
	WmfRestoreDC				= 65831,
	WmfInvertRegion				= 65834,
	WmfPaintRegion				= 65835,
	WmfSelectClipRegion			= 65836,
	WmfSelectObject				= 65837,
	WmfSetTextAlign				= 65838,
	WmfResizePalette			= 65849,
	WmfDibCreatePatternBrush	= 65858,
	WmfSetLayout				= 65865,
	WmfDeleteObject				= 66032,
	WmfCreatePatternBrush		= 66041,
	WmfSetBkColor				= 66049,
	WmfSetTextColor				= 66057,
	WmfSetTextJustification		= 66058,
	WmfSetWindowOrg				= 66059,
	WmfSetWindowExt				= 66060,
	WmfSetViewportOrg			= 66061,
	WmfSetViewportExt			= 66062,
	WmfOffsetWindowOrg			= 66063,
	WmfOffsetViewportOrg		= 66065,
	WmfLineTo					= 66067,
	WmfMoveTo					= 66068,
	WmfOffsetCilpRgn			= 66080,
	WmfFillRegion				= 66088,
	WmfSetMapperFlags			= 66097,
	WmfSelectPalette			= 66100,
	WmfCreatePenIndirect		= 66298,
	WmfCreateFontIndirect		= 66299,
	WmfCreateBrushIndirect		= 66300,
	WmfPolygon					= 66340,
	WmfPolyline					= 66341,
	WmfScaleWindowExt			= 66576,
	WmfScaleViewportExt			= 66578,
	WmfExcludeClipRect			= 66581,
	WmfIntersectClipRect		= 66582,
	WmfFloodFill				= 66585,
	WmfEllipse					= 66584,
	WmfRectangle				= 66587,
	WmfSetPixel					= 66591,
	WmfFrameRegion				= 66601,
	WmfAnimatePalette			= 66614,
	WmfTextOut					= 66849,
	WmfPolyPolygon				= 66872,
	WmfExtFloodFill				= 66888,
	WmfRoundRect				= 67100,
	WmfPatBlt					= 67101,
	WmfEscape					= 67110,
	WmfCreateRegion				= 67327,
	WmfArc						= 67607,
	WmfPie						= 67610,
	WmfChord					= 67632,
	WmfBitBlt					= 67874,
	WmfDibBitBlt				= 67904,
	WmfExtTextOut				= 68146,
	WmfStretchBlt				= 68387,
	WmfDibStretchBlt			= 68417,
	WmfSetDibToDev				= 68915,
	WmfStretchDib				= 69443

}; // enum EmfPlusRecordType

}; // namespace System.Drawing.Imaging
