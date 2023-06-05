using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VisibleTest;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
        BindingContext = new Clicky();
        
        InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

        ((Clicky)BindingContext).IsZero = count == 0;
        ((Clicky)BindingContext).IsNotZero = count > 0;

        SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

public class Clicky : INotifyPropertyChanged
{
    protected bool SetProperty<T>(ref T backingStore, T value,
        [CallerMemberName] string propertyName = "",
        Action onChanged = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);
        return true;
    }

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        var changed = PropertyChanged;
        if (changed == null)
            return;

        changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    bool isZero = true;
    public bool IsZero
    {
        get => isZero;
        set
        {
            SetProperty(ref isZero, value);
        }
    }

    bool isNotZero;
    public bool IsNotZero
    {
        get => isNotZero;
        set
        {
            SetProperty(ref isNotZero, value);
        }
    }
}



