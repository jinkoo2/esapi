using System;
using System.Windows.Forms;
using Kitware.VTK;

namespace esapi.UI
{
    public class ImageOneViewUserControl : UserControl
    {
        private RenderWindowControl renderWindowControl;

        public ImageOneViewUserControl()
        {
            renderWindowControl = new RenderWindowControl
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(renderWindowControl);

            renderWindowControl.Load += (_, __) => InitializeVTK();
        }

        private void InitializeVTK()
        {
            vtkSphereSource sphere = vtkSphereSource.New();
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(sphere.GetOutputPort());

            vtkActor actor = vtkActor.New();
            actor.SetMapper(mapper);

            vtkRenderer renderer = renderWindowControl.RenderWindow.GetRenderers().GetFirstRenderer();
            renderer.AddActor(actor);
            renderer.SetBackground(0.1, 0.2, 0.4);
            renderWindowControl.RenderWindow.Render();
        }
    }

}
