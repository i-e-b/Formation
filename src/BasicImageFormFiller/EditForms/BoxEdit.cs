﻿using System;
using System.Windows.Forms;
using BasicImageFormFiller.FileFormats;

namespace BasicImageFormFiller.EditForms
{
    public partial class BoxEdit : Form
    {
        private readonly Project? _project;
        private readonly int _pageIndex;
        private readonly string _boxKey;

        public BoxEdit()
        {
            _boxKey = "";
            InitializeComponent();
        }

        public BoxEdit (Project project, int pageIndex, string boxKey)
        {
            InitializeComponent();
            _project = project;
            _pageIndex = pageIndex;
            _boxKey = boxKey;
            
            var box = project.Pages[pageIndex].Boxes[boxKey];
            wrapTextCheckbox!.Checked = box.WrapText;
            shrinkToFitCheckbox!.Checked = box.ShrinkToFit;
            boxKeyTextbox!.Text = boxKey;

            topLeft!.Checked = box.Alignment == TextAlignment.TopLeft;
            topCentre!.Checked = box.Alignment == TextAlignment.TopCentre;
            topRight!.Checked = box.Alignment == TextAlignment.TopRight;
            
            midLeft!.Checked = box.Alignment == TextAlignment.MidlineLeft;
            midCentre!.Checked = box.Alignment == TextAlignment.MidlineCentre;
            midRight!.Checked = box.Alignment == TextAlignment.MidlineRight;
            
            bottomLeft!.Checked = box.Alignment == TextAlignment.BottomLeft;
            bottomCentre!.Checked = box.Alignment == TextAlignment.BottomCentre;
            bottomRight!.Checked = box.Alignment == TextAlignment.BottomRight;
        }

        private void BoxEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var container = _project!.Pages[_pageIndex].Boxes;

            var box = _project!.Pages[_pageIndex].Boxes[_boxKey];
            box.WrapText = wrapTextCheckbox!.Checked;
            box.ShrinkToFit = shrinkToFitCheckbox!.Checked;

            if (topLeft!.Checked) box.Alignment = TextAlignment.TopLeft;
            else if (topCentre!.Checked) box.Alignment = TextAlignment.TopCentre;
            else if (topRight!.Checked) box.Alignment = TextAlignment.TopRight;
            
            else if (midLeft!.Checked) box.Alignment = TextAlignment.MidlineLeft;
            else if (midCentre!.Checked) box.Alignment = TextAlignment.MidlineCentre;
            else if (midRight!.Checked) box.Alignment = TextAlignment.MidlineRight;
            
            else if (bottomLeft!.Checked) box.Alignment = TextAlignment.BottomLeft;
            else if (bottomCentre!.Checked) box.Alignment = TextAlignment.BottomCentre;
            else if (bottomRight!.Checked) box.Alignment = TextAlignment.BottomRight;
            else box.Alignment = TextAlignment.MidlineLeft;
            
            
            // If key has changed, try updating
            var newKey = boxKeyTextbox!.Text;
            // if key has changed and it's not already in use:
            if (newKey != _boxKey && !string.IsNullOrWhiteSpace(newKey) && !container.ContainsKey(newKey)) 
            {
                var tmp = container[_boxKey];
                container.Remove(_boxKey);
                container.Add(newKey, tmp);
            }
            
            _project?.Save();
            Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            _project!.Pages[_pageIndex].Boxes.Remove(_boxKey);
            _project?.Save();
            Close();
        }
    }
}