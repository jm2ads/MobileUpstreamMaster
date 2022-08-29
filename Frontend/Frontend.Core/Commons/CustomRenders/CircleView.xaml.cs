using Xamarin.Forms;
using SkiaSharp.Views.Forms;
using SkiaSharp;

namespace Frontend.Core.Commons.CustomRenders
{
    public partial class CircleView : ContentView
    {
        public static readonly BindableProperty CircleColorProperty = BindableProperty.Create(
            propertyName: "CircleColor",
            returnType: typeof(Color),
            declaringType: typeof(CircleView),
            defaultValue: Color.Blue,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: ColorPropertyChanged
        );

        public Color CircleColor
        {
            get { return (Color)base.GetValue(CircleColorProperty); }
            set { base.SetValue(CircleColorProperty, value); }
        }

        private static void ColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CircleView)bindable;
        }

        public CircleView()
        {
            InitializeComponent();
            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = CircleColor.ToSKColor(),
                StrokeWidth = 25
            };
            canvas.DrawCircle(info.Width / 2, info.Height / 2, 18, paint);
        }
    }
}
