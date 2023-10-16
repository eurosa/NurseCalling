using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NurseCalling
{
    public class ModbusRTU
    {
        public ModbusRTU() 
        {

            SerialPort port = new SerialPort("COM1");

            // configure serial port
            port.BaudRate = 9600;
            port.DataBits = 8;
            port.Parity = Parity.None;
            port.StopBits = StopBits.One;
            port.Open();

            // create modbus master
            IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

            byte slaveId = 1;
            ushort startAddress = 1;
            ushort numRegisters = 5;

            // read five registers
            ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);

            for (int i = 0; i < numRegisters; i++)
                Console.WriteLine("Register {0}={1}", startAddress + i, registers[i]);

            Debug.WriteLine(slaveId+" ", startAddress, new bool[] { true, false, true });
            // write three coils
            master.WriteMultipleCoils(slaveId, startAddress, new bool[] { true, false, true });

        }
    }
}
