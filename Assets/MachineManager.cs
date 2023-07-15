using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    [System.Serializable]
    class Phase {
        public List<MachineScript> machineScripts;
    }
    [SerializeField] Color offColor = Color.black;
    [SerializeField] Color onColor = Color.white;
    [SerializeField] List<Phase> m_phases = new List<Phase>();
    [SerializeField] List<MachineScript> allMachines = new List<MachineScript>();
    int phaseIndex = 0;
    public void SetPhase() {
        phaseIndex = ClientOrderMGR.Instance.CurrentPhaseIndex;
        foreach (MachineScript machine in allMachines) {
            machine.machineActive = false;
            MeshRenderer[] meshRenderers = machine.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mesh in meshRenderers) {

                foreach (Material mat in mesh.materials) {
                    mat.color = offColor;
                } 
            }
        }
        foreach (MachineScript machine in m_phases[phaseIndex].machineScripts) {
            machine.machineActive = true;
            MeshRenderer[] meshRenderers = machine.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mesh in meshRenderers) {

                foreach (Material mat in mesh.materials) {
                    mat.color = onColor;
                }
            }

        }
    }
}
