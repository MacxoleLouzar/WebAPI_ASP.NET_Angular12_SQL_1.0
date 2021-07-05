import { Component, OnInit, Input } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-add-edit-dep',
  templateUrl: './add-edit-dep.component.html',
  styleUrls: ['./add-edit-dep.component.css']
})
export class AddEditDepComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() dep:any;
  DepartmentId!: string;
  DpartmentName!:string;

  ngOnInit(): void {
    this.DepartmentId=this.dep.DepartmentId;
    this.DpartmentName=this.dep.DpartmentName;
  }
  addDepartment(){
   var val = {DepartmentId:this.dep.DepartmentId,
              DepartmentName:this.dep.DpartmentName};
              this.service.addDepartment(val).subscribe(res=>{
                alert(res.toString());
  });
}
  updateDepartment(){
    var val = {DepartmentId:this.dep.DepartmentId,
      DepartmentName:this.dep.DpartmentName};
      this.service.updateDepartment(val).subscribe(res=>{
        alert(res.toString());
});
  }

}
