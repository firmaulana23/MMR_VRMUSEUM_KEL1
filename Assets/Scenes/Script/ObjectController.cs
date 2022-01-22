//-----------------------------------------------------------------------
// <copyright file="ObjectController.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using UnityEngine;

/// <summary>
/// Controls target objects behaviour.
/// </summary>
public class ObjectController : MonoBehaviour
{
    /// <summary>
    /// The material to use when this object is inactive (not being gazed at).
    /// </summary>
    public Material InactiveMaterial;

    /// <summary>
    /// The material to use when this object is active (gazed at).
    /// </summary>
    public Material GazedAtMaterial;

    // The objects are about 1 meter in radius, so the min/max target distance are
    // set so that the objects are always within the room (which is about 5 meters
    // across).
    private const float _minObjectDistance = 2.5f;
    private const float _maxObjectDistance = 3.5f;
    private const float _minObjectHeight = 0.5f;
    private const float _maxObjectHeight = 3.5f;
    private const float x_place=-2.1f;
    private const float y_place=1.1f;
    private const float z_place=-0.9f;
    int time,timeOut;
    bool gazed;
    bool stat;
    public SoundManager sm;
    private Renderer _myRenderer;
    private Vector3 _startingPosition;
    private bool change=false;
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {
        _startingPosition = transform.parent.localPosition;
        _myRenderer = GetComponent<Renderer>();
        SetMaterial(false);
        Init();
    }

    private void Init(){
        gazed = false;
        time = 0;
        timeOut=0;
        stat=false;
    }

    private void FixedUpdate(){
        if(gazed && stat==false){
            time++;
            if(time > 10){
                // if(change)
                zoom_in();
                stat=true;
                // else zoom_out();
                time = 0;
                // gazed = false;
            }
        }   
        else if(!gazed && stat==true){
            timeOut++;
            if(timeOut > 10){
               
                zoom_out();
                stat=false;
                
                timeOut = 0;
                gazed = false;
            }
        } 
    }

    /// <summary>
    /// Teleports this instance randomly when triggered by a pointer click.
    /// </summary>
    public void zoom_in()
    {
        // Picks a random sibling, activates it and deactivates itself.
        // int sibIdx = transform.GetSiblingIndex();
        // int numSibs = transform.parent.childCount;
        // sibIdx = (sibIdx + Random.Range(1, numSibs)) % numSibs;
        // // GameObject randomSib = transform.parent.GetChild(sibIdx).gameObject;

        // // Computes new object's location.
        // float angle = Random.Range(-Mathf.PI, Mathf.PI);
        // float distance = Random.Range(_minObjectDistance, _maxObjectDistance);
        // float height = Random.Range(_minObjectHeight, _maxObjectHeight);
        // Vector3 newPos = new Vector3(Mathf.Cos(angle) * distance, height,
        //                              Mathf.Sin(angle) * distance);

        // Moves the parent to the new position (siblings relative distance from their parent is 0).
        // transform.parent.localPosition = newPos;

   
        float x=-1.116f;
        float y=1.114f;
        float z=-0.55f;
        
        Vector3 pos= new Vector3(x,y,z);
        gameObject.transform.position=pos;
        sm.Play(0);
        // gameObject.SetActive(false);
        // SetMaterial(false);
    }
    public void zoom_out(){
        
        Vector3 pos= new Vector3(x_place,y_place,z_place);
        gameObject.transform.position=pos;
    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
        SetMaterial(true);
        gazed = true;
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        
        SetMaterial(false);
        gazed = false;
        // change=false;
        // zoom_out();
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
        
        zoom_in();
    }

    /// <summary>
    /// Sets this instance's material according to gazedAt status.
    /// </summary>
    ///
    /// <param name="gazedAt">
    /// Value `true` if this object is being gazed at, `false` otherwise.
    /// </param>
    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
        }
    }
}
