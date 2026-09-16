using System;
using System.Windows;
using System.Windows.Input;
using Empyeesln;
using System.Windows.Input;


namespace Employeesln { 

public class EmployeeViewModel : ViewModelBase
    {
    
    public string ButtonContent;
    private ScreenValues screenValues;
    private ICommand c_ButtonCommand;
    private string? _screenNotify;
    private string? _setScreenValue;

     
    public ICommand ButtonCommand
    {
        get
        {
            return c_ButtonCommand;
        }
        set
        {
            c_ButtonCommand = value;
        }
    }
    public string ScreenNotify
    {
        get => _screenNotify;

        set
        {
            if (_screenNotify != value)
            {
                _screenNotify = value;
                NotifyPropertyChanged("ScreenNotify");
            }

        }
    }
    public string SetScreenValue
    {
        get => _screenNotify;
        set
        {
            _setScreenValue = value;
        }
    }
    public ScreenValues ScreenValues
    {
        get
        {
            return screenValues;
        }
        set
        {
            screenValues = value;
            NotifyPropertyChanged("ScreenValues");
            screenValues.Screenmessage = "Please only numeric value expected";
            ScreenValues.Display = true;

        }
    }
    Employee

    public ICommand SubmitCommand
    {
        get
        {
            if (c_ButtonCommand == null)
            {
                c_ButtonCommand = new RelayCommand(param => this.Button_Click(), null
                    );
            }
            return c_ButtonCommand;
        }
    }
    public GenericViewModel()
    {
        ScreenValues = new ScreenValues();
            Jobs = new Jobs();

    }
    public void Button_Click()
    {
        if (!float.TryParse(ScreenValues.FsrtValue, out float value1)
           || !float.TryParse(ScreenValues.SCdValue, out float value)
           )
        {
            ScreenValues.SumValue = "Please only numeric value expected";

        }
        else
        {
            ScreenValues.SumValue = (value + value1).ToString();
            ScreenValues.Display = false;


        }
        NotifyPropertyChanged("ScreenValues");
    }
}

}